using DagAir.IngestionNode.Data.Influx;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.IngestionNode.Data
{
    public static class IngestionNodeDataFeature
    {
        public static IServiceCollection AddIngestionNodeDataFeature(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IInfluxConfiguration>(x =>
                InfluxConfiguration.GetConfiguration(configuration, "DagAirInfluxConfiguration"));

            return services;
        }
    }
}