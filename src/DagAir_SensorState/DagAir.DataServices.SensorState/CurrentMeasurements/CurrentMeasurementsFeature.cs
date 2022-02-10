using DagAir.DataServices.SensorState.CurrentMeasurements.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.DataServices.SensorState.CurrentMeasurements
{
    public static class CurrentMeasurementsFeature
    {
        public static IServiceCollection AddCurrentMeasurementsFeature(this IServiceCollection services)
        {
            services.AddScoped<IGetCurrentMeasurementHandler, GetCurrentMeasurementHandler>();

            return services;
        }
    }
}