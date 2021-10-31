using DagAir.Policies.Data.AppContext;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Policies.Data
{
    public static class DagAirPoliciesAppContextFeature
    {
        public static IServiceCollection AddDagAirPoliciesAppDbContext(this IServiceCollection services)
        {
            services.AddSingleton<DagAirPoliciesAppContextProvider>();
            services.AddScoped<IDagAirPoliciesAppContext, DagAirPoliciesAppContext>(x =>
            {
                var provider = x.GetRequiredService<DagAirPoliciesAppContextProvider>();
                return provider.Provide();
            });

            return services;
        }
    }
}