using DagAir.Sensors.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Sensors
{
    public static class SensorsFeature
    {
        public static IServiceCollection AddSensorsFeature(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDagAirAppDbContext();
            return services;
        }
    }
}