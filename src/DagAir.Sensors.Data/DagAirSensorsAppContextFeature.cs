using DagAir.Sensors.Data.AppContext;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Sensors.Data
{
    public static class DagAirSensorsAppContextFeature
    {
        public static IServiceCollection AddDagAirAppDbContext(this IServiceCollection services)
        {
            services.AddSingleton<DagAirSensorAppContextProvider>();
            services.AddScoped<IDagAirSensorAppContext, DagAirSensorAppContext>(x =>
            {
                var provider = x.GetRequiredService<DagAirSensorAppContextProvider>();
                return provider.Provide();
            });

            return services;
        }
    }
}