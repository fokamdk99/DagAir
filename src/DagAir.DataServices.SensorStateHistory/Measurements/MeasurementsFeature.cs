using DagAir.DataServices.SensorStateHistory.Measurements.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.DataServices.SensorStateHistory.Measurements
{
    public static class MeasurementsFeature
    {
        public static IServiceCollection AddAffiliatesEntitiesFeature(this IServiceCollection services)
        {
            services.AddScoped<IGetMeasurementsFromSensorQuery, GetMeasurementsFromSensorQuery>();

            return services;
        }
    }
}