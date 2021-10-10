using DagAir.MassTransit.RabbitMq;
using DagAir.MassTransit.RabbitMq.Configuration;
using DagAir.MassTransit.RabbitMq.Publisher;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.IngestionNode.Infrastructure
{
    public static class MassTransitFeature
    {
        public static IServiceCollection AddIngestionNodeMassTransitFeature(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEventPublisher, EventPublisher>();

            services.AddMassTransitFeature<IRabbitMqConfiguration>(configuration);

            return services;
        }
    }
}