namespace DagAir.MassTransit.RabbitMq
{
    public interface IRabbitMqConfiguration
    {
        string Host { get; }
        string UserName { get; }
        string Password { get; }
        string ConnectionName { get; }
        
    }
}