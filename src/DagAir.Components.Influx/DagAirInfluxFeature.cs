using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Components.Influx
{
    public static class DagAirInfluxFeature
    {
        public static IServiceCollection AddDagInfluxFeature(this IServiceCollection services)
        {
            services.AddScoped<IInfluxHelper, InfluxHelper>();

            return services;
        }
    }
}