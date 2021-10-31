using DagAir.IngestionNode.Data;
using DagAir.IngestionNode.Influx;
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
            services.AddIngestionNodeDataFeature(configuration);
            services.AddIngestionNodeRabbitMqFeature(configuration);
            services.AddInfluxCommandsFeature();
            services.AddIngestionNodeMeasurementsFeature();
            services.AddIngestionNodeIntegrationsFeature(configuration);

            return services;
        }
    }
}