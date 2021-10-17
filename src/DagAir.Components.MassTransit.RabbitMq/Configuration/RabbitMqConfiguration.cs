using Microsoft.Extensions.Configuration;

namespace DagAir.Components.MassTransit.RabbitMq.Configuration
{
    public class RabbitMqConfiguration : IRabbitMqConfiguration
    {
        public string HostName { get; private set; }
        public string VirtualHost { get; private set; }
        public int? MaxNumberOfRetries { get; private set; }
        public string ConnectionName { get; private set; }
        public string Protocol { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }

        public static RabbitMqConfiguration GetConfiguration(IConfiguration configuration, string sectionName)
        {
            var rabbitMqConfiguration = new RabbitMqConfiguration();
            configuration
                .GetSection(sectionName)
                .Bind(rabbitMqConfiguration, options => options.BindNonPublicProperties = true);

            return rabbitMqConfiguration;
        }
         
    }
}