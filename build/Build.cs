using DagAir.Components.Nuke.Components;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
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
        .Executes(() =>
        {
            DotNetRestore(s => s.EnsureNotNull(this as IHaveSolution, (_, o) => s.SetProjectFile(o.Solution)));
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
        .DependsOn(Compile)
        .Executes(() =>
        {
            var solution = (this as IHaveSolution).Solution;
            DotNet($"test {solution} --no-build -c {Configuration}");
        });
}
