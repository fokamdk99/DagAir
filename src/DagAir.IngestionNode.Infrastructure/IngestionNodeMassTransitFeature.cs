using System.Reflection;
using DagAir.MassTransit.RabbitMq;
using DagAir.MassTransit.RabbitMq.Configuration;
using DagAir.MassTransit.RabbitMq.Publisher;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.IngestionNode.Infrastructure
{
    public static class MassTransitFeature
    {
        public static IServiceCollection AddIngestionNodeMassTransitFeature(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
        {
            services.AddScoped<IEventPublisher, EventPublisher>();

            services.AddMassTransitFeature<IRabbitMqConfiguration>(configuration, IngestionNodeMassTransitExtensions.ConfigureRabbitMqBus);

            return services;
        }
    }
}