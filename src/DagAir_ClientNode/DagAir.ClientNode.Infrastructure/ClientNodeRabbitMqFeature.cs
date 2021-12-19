using DagAir.Components.MassTransit.RabbitMq.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.ClientNode.Infrastructure
{
    public static class ClientNodeRabbitMqFeature
    {
        public static IServiceCollection AddClientNodeRabbitMqFeature(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqConfiguration =
                RabbitMqConfiguration.GetConfiguration(configuration, "rabbitMq");
            services.AddSingleton<IRabbitMqConfiguration>(x => rabbitMqConfiguration);

            return services;
        }
    }
}