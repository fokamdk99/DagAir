using System.Reflection;
using DagAir.MassTransit.RabbitMq.Publisher;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.MassTransit.RabbitMq
{
    public static class MassTransitFeature
    {
        public static IServiceCollection AddMassTransitFeature(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
        {
            services.AddScoped<IEventPublisher, EventPublisher>();
            services.AddMassTransitHostedService();

            return services;
        }
    }
}