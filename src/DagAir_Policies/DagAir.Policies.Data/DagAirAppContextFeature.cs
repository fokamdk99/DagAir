using DagAir.Policies.Data.AppContext;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Policies.Data
{
    public static class DagAirAppContextFeature
    {
        public static IServiceCollection AddPoliciesDagAirAppDbContext(this IServiceCollection services)
        {
            services.AddSingleton<DagAirAppContextProvider>();
            services.AddScoped<IDagAirAppContext, DagAirAppContext>(x =>
            {
                var provider = x.GetRequiredService<DagAirAppContextProvider>();
                return provider.Provide();
            });

            return services;
        }
    }
}