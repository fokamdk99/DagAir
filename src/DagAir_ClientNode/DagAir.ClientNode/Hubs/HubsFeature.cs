using Microsoft.Extensions.DependencyInjection;

namespace DagAir.ClientNode.Hubs
{
    public static class HubsFeature
    {
        public static IServiceCollection AddHubsFeature(this IServiceCollection services)
        {
            //services.AddScoped<IClientNodeHub, ClientNodeHub>();

            return services;
        }
    }
}