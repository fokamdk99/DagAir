using DagAir.PolicyNode.Integrations.Policies.DataServices;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.PolicyNode.Integrations
{
    public static class PoliciesIntegrationsFeature
    {
        public static IServiceCollection AddPoliciesIntegrationsFeature(this IServiceCollection services)
        {
            services.AddScoped<IPoliciesDataService, PoliciesDataService>();
            services.AddSingleton<IServicesUrls, ServicesUrls>();

            return services;
        }
    }
}