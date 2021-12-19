using DagAir.AdminNode.Infrastructure;
using DagAir.Components.HttpClients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.AdminNode
{
    public static class AdminNodeFeature
    {
        public static IServiceCollection AddAdminNodeFeature(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAdminNodeInfrastructureFeature();
            services.AddDagAirHttpClientsFeature();
            
            return services;
        }
    }
}