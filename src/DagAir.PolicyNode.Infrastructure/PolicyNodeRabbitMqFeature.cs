using DagAir.MassTransit.RabbitMq.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.PolicyNode.Infrastructure
{
    public static class PolicyNodeRabbitMqFeature
    {
        public static IServiceCollection AddPolicyNodeRabbitMqFeature(this IServiceCollection services, IConfiguration configuration)
        {
            var sensorRabbitMqConfiguration =
                RabbitMqConfiguration.GetConfiguration(configuration, "rabbitMq");
            services.AddSingleton<IRabbitMqConfiguration>(x => sensorRabbitMqConfiguration);

            return services;
        }
    }
}