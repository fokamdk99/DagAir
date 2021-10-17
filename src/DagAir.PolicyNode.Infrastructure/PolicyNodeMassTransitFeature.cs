using System.Reflection;
using DagAir.MassTransit.RabbitMq;
using DagAir.Components.MassTransit.RabbitMq.Configuration;
using DagAir.Components.MassTransit.RabbitMq.Publisher;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.PolicyNode.Infrastructure
{
    public static class PolicyNodeMassTransitFeature
    {
        public static IServiceCollection AddPolicyNodeMassTransitFeature(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
        {
            services.AddScoped<IEventPublisher, EventPublisher>();

            services.AddPolicyNodeRabbitMqFeature(configuration);

            services.AddMassTransitFeature<IRabbitMqConfiguration>(configuration, PolicyNodeMassTransitExtensions.ConfigureRabbitMqBus, x => PolicyNodeMassTransitExtensions.AddServices(x, assembly));

            return services;
        }
    }
}