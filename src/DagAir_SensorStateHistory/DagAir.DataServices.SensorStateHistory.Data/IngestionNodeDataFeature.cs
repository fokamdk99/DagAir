﻿using DagAir.Components.Influx;
using InfluxDB.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.DataServices.SensorStateHistory.Data
{
    public static class IngestionNodeDataFeature
    {
        public static IServiceCollection AddIngestionNodeDataFeature(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IInfluxConfiguration>(x =>
                InfluxConfiguration.GetConfiguration(configuration, "DagAirInfluxConfiguration"));
            services.AddSingleton<InfluxDBClient>(x =>
            {
                var influxConfiguration = services.BuildServiceProvider().GetRequiredService<IInfluxConfiguration>();
                return InfluxDBClientFactory.Create(influxConfiguration.Url, influxConfiguration.Token);
            });

            return services;
        }
    }
}