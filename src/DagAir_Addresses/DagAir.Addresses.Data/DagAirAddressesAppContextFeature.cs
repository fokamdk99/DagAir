using DagAir.Addresses.Data.AppContext;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Addresses.Data
{
    public static class DagAirAddressesAppContextFeature
    {
        public static IServiceCollection AddDagAirAddressesAppDbContext(this IServiceCollection services)
        {
            services.AddSingleton<DagAirAddressesAppContextProvider>();
            services.AddScoped<IDagAirAddressesAppContext, DagAirAddressesAppContext>(x =>
            {
                var provider = x.GetRequiredService<DagAirAddressesAppContextProvider>();
                return provider.Provide();
            });

            return services;
        }
    }
}