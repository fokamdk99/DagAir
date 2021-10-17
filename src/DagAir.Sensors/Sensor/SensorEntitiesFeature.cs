using DagAir.Sensors.Sensor.Models;
using DagAir.Sensors.Sensor.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Sensors.Sensor
{
    public static class SensorEntitiesFeature
    {
        public static IServiceCollection AddSensorEntitiesFeature(this IServiceCollection services)
        {
            services.AddScoped<IQuery<SensorReadModel>, GetCurrentSensorsWithRelatedEntitiesQuery>();
            
            return services;
        }
    }
}