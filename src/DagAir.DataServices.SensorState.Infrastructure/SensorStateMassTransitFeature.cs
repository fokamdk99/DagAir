using System.Reflection;
using DagAir.Components.MassTransit.RabbitMq.Configuration;
using DagAir.Components.MassTransit.RabbitMq.Publisher;
using DagAir.MassTransit.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.DataServices.SensorState.Infrastructure
{
    public static class MassTransitFeature
    {
        public static IServiceCollection AddSensorStateMassTransitFeature(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
        {
            services.AddScoped<IEventPublisher, EventPublisher>();

            services.AddMassTransitFeature<IRabbitMqConfiguration>(configuration, SensorStateMassTransitExtensions.ConfigureRabbitMqBus, x => SensorStateMassTransitExtensions.AddServices(x, assembly));

            return services;
        }
    }
}