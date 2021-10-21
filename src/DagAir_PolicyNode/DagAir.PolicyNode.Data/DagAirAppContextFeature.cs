using DagAir.PolicyNode.Data.AppContext;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.PolicyNode.Data
{
    public static class DagAirAppContextFeature
    {
        public static IServiceCollection AddDagAirAppDbContext(this IServiceCollection services)
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