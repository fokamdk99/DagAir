using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.DataServices.SensorState.Integrations
{
    public static class SensorStateIntegrationsFeature
    {
        public static IServiceCollection AddSensorStateIntegrationsFeature(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IServicesUrls>(x => new ServicesUrls(configuration));

            return services;
        }
    }
}