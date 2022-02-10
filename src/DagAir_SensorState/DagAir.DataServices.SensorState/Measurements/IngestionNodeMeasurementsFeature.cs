using DagAir.DataServices.SensorState.Measurements.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.DataServices.SensorState.Measurements
{
    public static class SensorStateMeasurementsFeature
    {
        public static IServiceCollection AddSensorStateMeasurementsFeature(this IServiceCollection services)
        {
            services.AddScoped<INewMeasurementReceivedHandler, NewMeasurementReceivedHandler>();
            services.AddScoped<IMeasurementDeserializer, MeasurementDeserializer>();

            return services;
        }
    }
}