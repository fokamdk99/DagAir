using DagAir.Components.MassTransit.RabbitMq.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.DataServices.SensorStateHistory.Infrastructure
{
    public static class SensorStateHistoryRabbitMqFeature
    {
        public static IServiceCollection AddSensorStateHistoryRabbitMqFeature(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqConfiguration =
                RabbitMqConfiguration.GetConfiguration(configuration, "rabbitMq");
            services.AddSingleton<IRabbitMqConfiguration>(x => rabbitMqConfiguration);

            return services;
        }
    }
}