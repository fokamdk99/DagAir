using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using DagAir.Addresses.Infrastructure;
using DagAir.AdminNode.Infrastructure;
using DagAir.Components.Nuke.Components;
using DagAir.Components.Nuke.Tasks;
using DagAir.Facilities.Infrastructure;
using DagAir.Policies.Infrastructure;
using DagAir.Sensors.Infrastructure;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Docker;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild, IHaveSolution, IHaveGitRepository
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.GenerateSwaggerDocumentation);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = Configuration.Release;

    [Parameter("Project name to run specified target on")] 
    readonly string ProjectName;

    [GitVersion(Framework = "net5.0", NoFetch = true)] readonly GitVersion GitVersion;

    readonly Dictionary<string, string> TestProjectNames = new Dictionary<string, string>
    {
        {"ingestionNode","DagAir.IngestionNode.Tests"},
        {"policyNode","DagAir.PolicyNode.Tests"},
        {"sensors","DagAir.Sensors.Tests"},
        {"policies","DagAir.Policies.Tests"},
        {"webClientApp", "DagAir.WebClientApp.Tests"},
        {"clientNode", "DagAir.ClientNode.Tests"},
        {"facilities", "DagAir.Facilities.Tests"},
        {"addresses", "DagAir.Addresses.Tests"}
    };
    
    readonly Dictionary<string, string> ProjectNames = new Dictionary<string, string>
    {
        {"ingestionNode","DagAir.IngestionNode"},
        {"policyNode","DagAir.PolicyNode"},
        {"sensors","DagAir.Sensors"},
        {"policies","DagAir.Policies"}, 
        {"webClientApp", "DagAir.WebClientApp"},
        {"clientNode", "DagAir.ClientNode"},
        {"facilities", "DagAir.Facilities"},
        {"addresses", "DagAir.Addresses"}
    };

    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
    AbsolutePath AdminNodeProjectDirectory => SourceDirectory / "DagAir_AdminNode/DagAir.AdminNode";
    AbsolutePath ClientNodeProjectDirectory => SourceDirectory / "DagAir_ClientNode/DagAir.ClientNode";
    AbsolutePath AddressesApiProjectDirectory => SourceDirectory / "DagAir_Addresses/DagAir.Addresses";
    AbsolutePath FacilitiesApiProjectDirectory => SourceDirectory / "DagAir_Facilities/DagAir.Facilities";
    AbsolutePath PoliciesApiProjectDirectory => SourceDirectory / "DagAir_Policies/DagAir.Policies";
    AbsolutePath SensorsApiProjectDirectory => SourceDirectory / "DagAir_Sensors/DagAir.Sensors";
    string AdminNode => "DagAir.AdminNode";
    string ClientNode => "DagAir.ClientNode";
    string AddressesApi => "DagAir.Addresses";
    string FacilitiesApi => "DagAir.Facilities";
    string PoliciesApi => "DagAir.Policies";
    string SensorsApi => "DagAir.Sensors";

    Target Clean => _ => _
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(ArtifactsDirectory);
        });

    
    Target RestoreTestProject => _ => _
        .DependsOn(Clean)
        .Requires(() => ProjectName)
        .Executes(() =>
        {
            var solution = (this as IHaveSolution).Solution;
            var project = solution.AllProjects.Single(x => x.Name == TestProjectNames[ProjectName]);
            DotNetRestore(s => s.EnsureNotNull(this as IHaveSolution, (_, o) => s.SetProjectFile(project)));
        });
    
    Target RestoreProject => _ => _
        .DependsOn(Clean)
        .Requires(() => ProjectName)
        .Executes(() =>
        {
            var solution = (this as IHaveSolution).Solution;
            var project = solution.AllProjects.Single(x => x.Name == ProjectNames[ProjectName]);
            DotNetRestore(s => s.EnsureNotNull(this as IHaveSolution, (_, o) => s.SetProjectFile(project)));
        });
    
    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(s => s.EnsureNotNull(this as IHaveSolution, (_, o) => s.SetProjectFile(o.Solution)));
        });
    
    Target CompileTestProject => _ => _
        .DependsOn(RestoreTestProject)
        .Executes(() =>
        {
            var solution = (this as IHaveSolution).Solution;
            var project = solution.AllProjects.Single(x => x.Name == TestProjectNames[ProjectName]);
            DotNetBuild(s => s
                .EnsureNotNull(this as IHaveSolution, (_, o) => s.SetProjectFile(project))
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });
    
    Target CompileProject => _ => _
        .DependsOn(RestoreProject)
        .Executes(() =>
        {
            var solution = (this as IHaveSolution).Solution;
            var project = solution.AllProjects.Single(x => x.Name == ProjectNames[ProjectName]);
            DotNetBuild(s => s
                .EnsureNotNull(this as IHaveSolution, (_, o) => s.SetProjectFile(project))
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });
    
    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .EnsureNotNull(this as IHaveSolution, (_, o) => s.SetProjectFile(o.Solution))
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target TestProject => _ => _
        .DependsOn(CompileTestProject)
        .Executes(() =>
        {
            var solution = (this as IHaveSolution).Solution;
            var project = solution.AllProjects.Single(x => x.Name == TestProjectNames[ProjectName]);
            DotNet($"test {project} --no-build -c {Configuration}");
        });

    Target TestAll => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            foreach (var projectName in TestProjectNames)
            {
                var solution = (this as IHaveSolution).Solution;
                var project = solution.AllProjects.Single(x => x.Name == projectName.Value);
                DotNet($"test {project} --no-build -c {Configuration}");
            }
        });

    Target PackProject => _ => _
        .DependsOn(CompileProject)
        .Executes(() =>
        {
            var solution = (this as IHaveSolution).Solution;
            var project = solution.AllProjects.Single(x => x.Name == ProjectNames[ProjectName]);
            var tempArtifactoryProjectPath = ArtifactsDirectory / project.Name;

            DotNetPublish(s =>
                s.SetOutput(tempArtifactoryProjectPath)
                    .SetProject(project.Path)
                    .SetConfiguration(Configuration)
                    .SetVersion(GitVersion.NuGetVersionV2)
                    .SetNoBuild(true));
        });
    Target PackAll => _ => _
        .Executes(() =>
        {
            foreach (var projectName in ProjectNames)
            {
                var solution = (this as IHaveSolution).Solution;
                var project = solution.AllProjects.Single(x => x.Name == projectName.Value);
                var tempArtifactoryProjectPath = ArtifactsDirectory / project.Name;

                DotNetPublish(s =>
                    s.SetOutput(tempArtifactoryProjectPath)
                        .SetProject(project.Path)
                        .SetConfiguration(Configuration)
                        .SetVersion(GitVersion.NuGetVersionV2)
                        .SetNoBuild(true));
            }
        });

    Target SetupLocally => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DockerComposeTasks.DockerCompose("-f docker-compose.local.infrastructure.yml pull -q", RootDirectory);
            DockerComposeTasks.DockerCompose("-f docker-compose.local.infrastructure.yml up -d", RootDirectory);
            Thread.Sleep(3000);
        })
        .Triggers(RunDatabaseMigration);

    Target RunDatabaseMigration => _ => _
        .DependsOn(SetupLocally)
        .Executes(() =>
        {
            var solution = (this as IHaveSolution).Solution;
            var projects = solution.AllProjects.Where(x => x.Name.Contains("Migrations"));
            foreach (var project in projects)
            {
                DotNetRun(settings => settings
                    .SetProjectFile(project.Path)
                    .SetProcessWorkingDirectory(project.Directory));
            }
        });

    Target GenerateSwaggerDocumentation => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            var projectList = new List<ProjectData>();
            projectList.Add(
                new ProjectData{ProjectPath = AdminNodeProjectDirectory, ProjectName = AdminNode, SwaggerDocName = AdminNodeApiVersions.AdminV1});
            projectList.Add(
                new ProjectData{ProjectPath = AddressesApiProjectDirectory, ProjectName = AddressesApi, SwaggerDocName = AddressesApiVersions.AddressesV1});
            projectList.Add(
                new ProjectData{ProjectPath = FacilitiesApiProjectDirectory, ProjectName = FacilitiesApi, SwaggerDocName = FacilitiesApiVersions.FacilitiesV1});
            projectList.Add(
                new ProjectData{ProjectPath = PoliciesApiProjectDirectory, ProjectName = PoliciesApi, SwaggerDocName = PoliciesApiVersions.PoliciesV1});
            projectList.Add(
                new ProjectData{ProjectPath = SensorsApiProjectDirectory, ProjectName = SensorsApi, SwaggerDocName = SensorsApiVersions.SensorsV1});

            foreach (var project in projectList)
            {
                var projectAssembly = project.ProjectPath / "bin" / Configuration / "net5.0" /
                                      $"{project.ProjectName}.dll";
                SwaggerTasks
                    .GenerateSwaggerDocs(
                        $"tofile --output {project.ProjectPath}/swagger-api.yaml --serializeasv2 --yaml {projectAssembly} {project.SwaggerDocName}", RootDirectory);

                CustomDockerTasks.GenerateAdocFile(
                    $"run --rm -v {project.ProjectPath}:/opt swagger2markup/swagger2markup convert -i /opt/swagger-api.yaml -f /opt/swagger");
            }
        });
}

class ProjectData
{
    public AbsolutePath ProjectPath { get; set; }
    public string ProjectName { get; set; }
    public string SwaggerDocName { get; set; }
}
