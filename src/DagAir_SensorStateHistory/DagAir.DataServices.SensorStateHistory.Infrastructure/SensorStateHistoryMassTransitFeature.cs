using System.Reflection;
using DagAir.Components.MassTransit.RabbitMq.Configuration;
using DagAir.Components.MassTransit.RabbitMq.Publisher;
using DagAir.MassTransit.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.DataServices.SensorStateHistory.Infrastructure
{
    public static class SensorStateHistoryMassTransitFeature
    {
        public static IServiceCollection AddSensorStateHistoryMassTransitFeature(this IServiceCollection services, 
            IConfiguration configuration, 
            Assembly assembly)
        {
            services.AddScoped<IEventPublisher, EventPublisher>();

            services.AddSensorStateHistoryRabbitMqFeature(configuration);

            services.AddMassTransitFeature<IRabbitMqConfiguration>(configuration, 
                SensorStateHistoryMassTransitExtensions.ConfigureRabbitMqBus, x => 
                    SensorStateHistoryMassTransitExtensions.AddServices(x, assembly));

            return services;
        }
    }
}