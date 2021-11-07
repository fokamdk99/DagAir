using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using DagAir.Components.Nuke.Components;
using DagAir.Components.Nuke.Tasks;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Docker;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild, IHaveSolution, IHaveGitRepository, IHaveGitVersion, IHaveProjectName
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = Configuration.Release;
    
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
        .Triggers(StartTestInfluxContainer)
        .Executes(() =>
        {
            DotNetRestore(s => s.EnsureNotNull(this as IHaveSolution, (_, o) => s.SetProjectFile(o.Solution)));
        });
    
    
    Target StartTestInfluxContainer => _ => _
        .After(Restore)
        .Executes(() =>
        {
            DockerComposeTasks.DockerCompose("-f docker-compose.tests.infrastructure.yml pull -q");
            DockerComposeTasks.DockerCompose("-f docker-compose.tests.infrastructure.yml up -d");

            bool isInfluxReady = false;
            for (int i = 0; i < 10; i++)
            {
                if (isInfluxReady)
                {
                    break;
                }
                var execSettings = new DockerExecSettings()
                    .SetContainer("influxdb")
                    .SetWorkdir("/usr/local/bin/")
                    .SetCommand("influx ping");
                
                var result = DockerTasks.DockerExec(execSettings);
                vara tmp1 = DockerTasks.Docker
                foreach (var output in result)
                {
                    if (output.Text == "OK")
                    {
                        isInfluxReady = true;
                        Logger.Info("Influxdb is ready!");
                    }
                    else
                    {
                        Thread.Sleep(2000);
                        Logger.Warn("Influxdb is not ready yet.");
                    }
                }
            }
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Triggers(Test)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .EnsureNotNull(this as IHaveSolution, (_, o) => s.SetProjectFile(o.Solution))
                .SetConfiguration(Configuration)
                
                .EnsureNotNull(this as IHaveGitVersion, (_, o) => _
                    .SetAssemblyVersion(o.GitVersion.AssemblySemVer)
                    .SetFileVersion(o.GitVersion.AssemblySemFileVer)
                    .SetInformationalVersion(o.GitVersion.InformationalVersion)
                )
                .EnableNoRestore());
        });
    
    
    Target Test => _ => _
        .DependsOn(Compile, StartTestInfluxContainer)
        .Triggers(RemoveTestInfluxContainer)
        .Executes(() =>
        {
            Thread.Sleep(10000);
            var solution = (this as IHaveSolution).Solution;
            DotNet($"test {solution} --no-build -c {Configuration}");
        });

    Target RemoveTestInfluxContainer => _ => _
        .DependsOn(Test)
        .Executes(() =>
        {
            DockerComposeTasks.DockerCompose("-f docker-compose.tests.infrastructure.yml rm -f -s");
        });
        
}
