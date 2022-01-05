using DagAir.Components.HttpClients;
using Microsoft.Extensions.DependencyInjection;
using WebAdminApp1.Infrastructure;

namespace WebAdminApp1
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