using DagAir.Components.MassTransit.RabbitMq.Configuration;
using DagAir.DataServices.SensorState.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.DataServices.SensorState.Infrastructure
{
    public static class RabbitMqFeature
    {
        public static IServiceCollection AddSensorStateRabbitMqFeature(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqConfiguration =
                RabbitMqConfiguration.GetConfiguration(configuration, "rabbitMq");
            services.AddSingleton<IRabbitMqConfiguration>(x => rabbitMqConfiguration);
            
            var sensorRabbitMqConfiguration =
                SensorRabbitMqConfiguration.GetSensorRabbitMqConfiguration(configuration, "sensorRabbitMq");
            services.AddSingleton<ISensorRabbitMqConfiguration>(x => sensorRabbitMqConfiguration);

            return services;
        }
    }
}