using DagAir.DataServices.SensorStateHistory.Influx.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.DataServices.SensorStateHistory.Influx
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