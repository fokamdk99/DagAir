using DagAir.Components.HttpClients;
using DagAir.DataServices.SensorState.CurrentMeasurements;
using DagAir.DataServices.SensorState.Infrastructure;
using DagAir.DataServices.SensorState.Integrations;
using DagAir.DataServices.SensorState.Measurements;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.DataServices.SensorState
{
    public static class SensorStateFeature
    {
        public static IServiceCollection AddSensorStateFeature(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddSensorStateRabbitMqFeature(configuration);
            services.AddSensorStateMeasurementsFeature();
            services.AddDagAirHttpClientsFeature();
            services.AddSensorStateIntegrationsFeature(configuration);
            services.AddCurrentMeasurementsFeature();

            return services;
        }
    }
}