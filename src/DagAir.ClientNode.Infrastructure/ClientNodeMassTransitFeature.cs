using System.Reflection;
using DagAir.Components.MassTransit.RabbitMq.Configuration;
using DagAir.Components.MassTransit.RabbitMq.Publisher;
using DagAir.MassTransit.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.ClientNode.Infrastructure
{
    public static class ClientNodeMassTransitFeature
    {
        public static IServiceCollection AddClientNodeMassTransitFeature(this IServiceCollection services,
            IConfiguration configuration,
            Assembly assembly)
        {
            services.AddScoped<IEventPublisher, EventPublisher>();
            
            services.AddMassTransitFeature<IRabbitMqConfiguration>(configuration, ClientNodeMassTransitExtensions.ConfigureRabbitMqBus, x => ClientNodeMassTransitExtensions.AddServices(x, assembly));
            
            return services;
        }
    }
}