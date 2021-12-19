using DagAir.Facilities.Data.AppContext;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Facilities.Data
{
    public static class DagAirSensorsAppContextFeature
    {
        public static IServiceCollection AddDagAirFacilitiesDbContext(this IServiceCollection services)
        {
            services.AddSingleton<DagAirFacilitiesAppContextProvider>();
            services.AddScoped<IDagAirFacilitiesAppContext, DagAirFacilitiesAppContext>(x =>
            {
                var provider = x.GetRequiredService<DagAirFacilitiesAppContextProvider>();
                return provider.Provide();
            });

            return services;
        }
    }
}