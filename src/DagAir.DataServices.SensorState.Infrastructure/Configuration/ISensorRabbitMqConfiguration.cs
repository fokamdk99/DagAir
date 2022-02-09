using DagAir.Components.MassTransit.RabbitMq.Configuration;

namespace DagAir.DataServices.SensorState.Infrastructure.Configuration
{
    public interface ISensorRabbitMqConfiguration : IRabbitMqConfiguration
    {
        string SensorExchange { get; }
        string RoutingKey { get; }
    }
}