using System;

namespace DagAir.Components.Nuke.Components
{
    public static class ToolSettingsExtensions
    {
        public static T EnsureNotNull<T, TObject>(
            this T settings,
            TObject obj,
            Func<T, TObject, T> configurator)
        {
            if (obj == null)
            {
                throw new NullReferenceException($"Build must implement {obj.GetType().Name}");
            }

            return configurator.Invoke(settings, obj);
        }
    }
}