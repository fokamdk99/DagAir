using DagAir.MassTransit.RabbitMq.Configuration;
using Microsoft.Extensions.Configuration;

namespace DagAir.IngestionNode.Infrastructure.Configuration
{
    public class SensorRabbitMqConfiguration : RabbitMqConfiguration, ISensorRabbitMqConfiguration
    {
        private const string SensorRabbitMqConfigurationKey = "sensorRabbitMqConfiguration";
        
        public string SensorExchange { get; private set; }
        public string RoutingKey { get; private set; }
        
        public static SensorRabbitMqConfiguration GetSensorRabbitMqConfiguration(IConfiguration configuration, string sectionName)
        {
            var rabbitMqConfiguration = new SensorRabbitMqConfiguration();
            configuration
                .GetSection(sectionName)
                .Bind(rabbitMqConfiguration, options => options.BindNonPublicProperties = true);

            return rabbitMqConfiguration;
        }
    }
}