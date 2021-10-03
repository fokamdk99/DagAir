namespace DagAir.MassTransit.RabbitMq.Configuration
{
    public interface IRabbitMqConfiguration
    {
        public string HostName { get; }
        public string VirtualHost { get; }
        public int? MaxNumberOfRetries { get; }
        public string ConnectionName { get; }
        public string Protocol { get; }
        public string UserName { get; }
        public string Password { get; }
        
    }
}