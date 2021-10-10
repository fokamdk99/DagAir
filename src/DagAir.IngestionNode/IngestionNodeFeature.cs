using DagAir.IngestionNode.InfluxCommands;
using DagAir.IngestionNode.Infrastructure;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.IngestionNode
{
    public static class IngestionNodeFeature
    {
        public static IServiceCollection AddIngestionNodeFeature(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddRabbitMqFeature(configuration);
            services.AddIngestionNodeMassTransitFeature(configuration);
            services.AddInfluxCommandsFeature();

            services.AddHostedService<Worker>();

            return services;
        }
    }
}