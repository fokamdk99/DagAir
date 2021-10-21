using DagAir.QueryNode.Data.AppContext;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.QueryNode.Data
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