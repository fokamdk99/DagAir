using System.Reflection;
using DagAir.MassTransit.RabbitMq;
using DagAir.Components.MassTransit.RabbitMq.Configuration;
using DagAir.Components.MassTransit.RabbitMq.Publisher;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.IngestionNode.Infrastructure
{
    public static class MassTransitFeature
    {
        public static IServiceCollection AddIngestionNodeMassTransitFeature(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
        {
            services.AddScoped<IEventPublisher, EventPublisher>();

            services.AddMassTransitFeature<IRabbitMqConfiguration>(configuration, IngestionNodeMassTransitExtensions.ConfigureRabbitMqBus, x => IngestionNodeMassTransitExtensions.AddServices(x, assembly));

            return services;
        }
    }
}