using Nuke.Common;
using Nuke.Common.Git;
using Nuke.Common.ValueInjection;

namespace DagAir.Components.Nuke.Components
{
    public interface IHaveGitRepository : INukeBuild
    {
        [GitRepository]
        [Required]
        public GitRepository GitRepository => ValueInjectionUtility.TryGetValue(() => GitRepository);
    }
}