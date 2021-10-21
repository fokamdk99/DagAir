using DagAir.IngestionNode.Influx.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.IngestionNode.Influx
{
    public static class InfluxCommandsFeature
    {
        public static IServiceCollection AddInfluxCommandsFeature(this IServiceCollection services)
        {
            services.AddScoped<ISaveMeasurementsToInfluxHandler, SaveMeasurementsToInfluxHandler>();

            return services;
        }
    }
}