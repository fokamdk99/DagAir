using DagAir.IngestionNode.Measurements.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.IngestionNode.Measurements
{
    public static class IngestionNodeMeasurementsFeature
    {
        public static IServiceCollection AddIngestionNodeMeasurementsFeature(this IServiceCollection services)
        {
            services.AddScoped<INewMeasurementReceivedHandler, NewMeasurementReceivedHandler>();
            services.AddScoped<IMeasurementDeserializer, MeasurementDeserializer>();

            return services;
        }
    }
}