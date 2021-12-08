using DagAir.Components.HttpClients;
using DagAir.WebApps.WebAdminApp2.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.WebApps.WebAdminApp2
{
    public static class WebAdminApp2Feature
    {
        public static IServiceCollection AddWebAdminApp2Feature(this IServiceCollection services)
        {
            services.AddWebAdminAppInfrastructureFeature();
            services.AddDagAirHttpClientsFeature();
            
            return services;
        }
    }
}