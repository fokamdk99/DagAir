using DagAir.Components.HttpClients;
using DagAir.IngestionNode.Infrastructure;
using DagAir.IngestionNode.Integrations;
using DagAir.IngestionNode.Measurements;
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
            services.AddIngestionNodeRabbitMqFeature(configuration);
            services.AddIngestionNodeMeasurementsFeature();
            services.AddDagAirHttpClientsFeature();
            services.AddIngestionNodeIntegrationsFeature(configuration);

            return services;
        }
    }
}