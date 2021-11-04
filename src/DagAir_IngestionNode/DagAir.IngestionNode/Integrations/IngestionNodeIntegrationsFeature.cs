using DagAir.IngestionNode.Integrations.Sensors.DataServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.IngestionNode.Integrations
{
    public static class IngestionNodeIntegrationsFeature
    {
        public static IServiceCollection AddIngestionNodeIntegrationsFeature(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IServicesUrls>(x => new ServicesUrls(configuration));
            services.AddScoped<ISensorsDataService, SensorsDataService>();

            return services;
        }
    }
}