using Nuke.Common;
using Nuke.Common.ProjectModel;
using static Nuke.Common.ValueInjection.ValueInjectionUtility;

namespace DagAir.Components.Nuke.Components
{
    public interface IHaveSolution : INukeBuild
    {
        [Solution] [Required] Solution Solution => TryGetValue(() => Solution);
    }
}