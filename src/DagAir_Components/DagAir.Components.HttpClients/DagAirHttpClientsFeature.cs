using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Components.HttpClients
{
    public static class DagAirHttpClientsFeature
    {
        public static IServiceCollection AddDagAirHttpClientsFeature(this IServiceCollection services)
        {
            services.AddHttpClient<DagAirHttpClient>();

            return services;
        }
    }
}