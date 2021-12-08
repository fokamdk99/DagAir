#nullable enable
using System.Reflection;
using DagAir.Components.MassTransit.RabbitMq.Configuration;
using DagAir.Components.MassTransit.RabbitMq.Publisher;
using DagAir.MassTransit.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.AdminNode.Infrastructure
{
    public static class AdminNodeMassTransitFeature
    {
        public static IServiceCollection AddAdminNodeMassTransitFeature(this IServiceCollection services,
            IConfiguration configuration,
            Assembly assembly)
        {
            services.AddScoped<IEventPublisher, EventPublisher>();

            services.AddAdminNodeRabbitMqFeature(configuration);
            
            services.AddMassTransitFeature<IRabbitMqConfiguration>(configuration, 
                AdminNodeMassTransitExtensions.ConfigureRabbitMqBus, 
                x => AdminNodeMassTransitExtensions.AddServices(x, assembly));
            
            return services;
        }
    }
}