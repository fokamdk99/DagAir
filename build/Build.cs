using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DagAir.Components.Nuke.Components;
using DagAir.Components.Nuke.Tasks;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
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

    public static int Main () => Execute<Build>(x => x.PackProject);

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
}
