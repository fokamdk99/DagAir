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
class Build : NukeBuild, IHaveSolution, IHaveGitRepository, IHaveProjectName
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.SetupLocally);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = Configuration.Release;

    [GitVersion(Framework = "net5.0", NoFetch = true)] readonly GitVersion GitVersion;
    
    public string ProjectName => "DagAir";

    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    Target Clean => _ => _
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(ArtifactsDirectory);
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(s => s.EnsureNotNull(this as IHaveSolution, (_, o) => s.SetProjectFile(o.Solution)));
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

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            var solution = (this as IHaveSolution).Solution;
            DotNet($"test {solution} --no-build -c {Configuration}");
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
