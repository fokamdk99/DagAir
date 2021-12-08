using DagAir.Components.MassTransit.RabbitMq.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.AdminNode.Infrastructure
{
    public static class AdminNodeRabbitMqFeature
    {
        public static IServiceCollection AddAdminNodeRabbitMqFeature(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqConfiguration =
                RabbitMqConfiguration.GetConfiguration(configuration, "rabbitMq");
            services.AddSingleton<IRabbitMqConfiguration>(x => rabbitMqConfiguration);

            return services;
        }
    }
}