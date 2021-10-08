using Microsoft.Extensions.DependencyInjection;

namespace DagAir.IngestionNode.InfluxCommands
{
    public static class InfluxCommandsFeature
    {
        public static IServiceCollection AddInfluxCommandsFeature(this IServiceCollection services)
        {
            services.AddScoped<ISaveMeasurementsToInfluxCommand, SaveMeasurementsToInfluxCommand>();

            return services;
        }
    }
}