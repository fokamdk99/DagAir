using Nuke.Common;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.ValueInjection;

namespace DagAir.Components.Nuke.Components
{
    public interface IHaveGitVersion : INukeBuild
    {
        [GitVersion(Framework = "net5.0", NoFetch = true)]
        [Required]
        public GitVersion GitVersion => ValueInjectionUtility.TryGetValue(() => GitVersion);
    }
}