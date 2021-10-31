using DagAir.Sensors.Sensors.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Sensors.Sensors
{
    public static class SensorEntitiesFeature
    {
        public static IServiceCollection AddSensorEntitiesFeature(this IServiceCollection services)
        {
            services.AddScoped<IGetSensorWithRelatedEntitiesQuery, GetSensorWithRelatedEntitiesQuery>();
            services.AddScoped<IGetAllSensorsWithRelatedEntitiesQuery, GetAllSensorsWithRelatedEntitiesQuery>();

            return services;
        }
    }
}