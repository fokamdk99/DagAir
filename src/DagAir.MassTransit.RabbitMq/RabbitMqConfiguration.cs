using Microsoft.Extensions.Configuration;

namespace DagAir.MassTransit.RabbitMq
{
    public class RabbitMqConfiguration : IRabbitMqConfiguration
    {
        public string Host { get; }
        public string UserName { get; }
        public string Password { get; }
        public string ConnectionName { get; }
        
        public RabbitMqConfiguration(IConfiguration cfg)
        {
            var hostName = cfg["rabbitMq:host"];
            var virtualHost = cfg["rabbitMq:virtualHost"];
            Host = $"rabbitmq://{hostName}/{virtualHost}";
            UserName = cfg["rabbitMq:userName"];
            Password = cfg["rabbitMq:password"];
            ConnectionName = cfg["rabbitMq:connectionName"];
        }
         
    }
}