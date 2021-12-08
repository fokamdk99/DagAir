using DagAir.Components.HttpClients;
using DagAir.WebAdminApp.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.WebAdminApp
{
    public static class WebAdminAppFeature
    {
        public static IServiceCollection AddWebAdminAppFeature(this IServiceCollection services)
        {
            services.AddWebAdminAppInfrastructureFeature();
            services.AddDagAirHttpClientsFeature();
            
            return services;
        }
    }
}