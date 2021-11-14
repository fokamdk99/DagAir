using DagAir.ClientNode.Hubs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.ClientNode
{
    public static class ClientNodeFeature
    {
        public static IServiceCollection AddClientNodeFeature(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHubsFeature();
            
            return services;
        }
    }
}