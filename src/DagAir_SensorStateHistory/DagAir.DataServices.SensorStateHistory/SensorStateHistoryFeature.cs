using DagAir.Components.Influx;
using DagAir.DataServices.SensorStateHistory.Influx;
using DagAir.DataServices.SensorStateHistory.Measurements;
using InfluxDB.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.DataServices.SensorStateHistory
{
    public static class SensorStateHistoryFeature
    {
        public static IServiceCollection AddSensorStateHistoryFeature(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IInfluxConfiguration>(x =>
                InfluxConfiguration.GetConfiguration(configuration, "DagAirInfluxConfiguration"));
            services.AddSingleton<InfluxDBClient>(x =>
            {
                var influxConfiguration = services.BuildServiceProvider().GetRequiredService<IInfluxConfiguration>();
                return InfluxDBClientFactory.Create(influxConfiguration.Url, influxConfiguration.Token);
            });

            services.AddDagInfluxFeature();
            services.AddInfluxCommandsFeature();
            services.AddAffiliatesEntitiesFeature();

            return services;
        }
    }
}