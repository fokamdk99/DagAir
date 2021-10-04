using DagAir.IngestionNode.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.IngestionNode.Infrastructure
{
    public static class RabbitMqFeature
    {
        public static IServiceCollection AddRabbitMqFeature(this IServiceCollection services, IConfiguration configuration)
        {
            var sensorRabbitMqConfiguration =
                SensorRabbitMqConfiguration.GetConfiguration(configuration, "sensorRabbitMqConfiguration");
            services.AddSingleton<ISensorRabbitMqConfiguration>(x => sensorRabbitMqConfiguration);

            return services;
        }
    }
}