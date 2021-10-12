using DagAir.Components.HealthChecks.BackGroundServices;
using DagAir.Components.HealthChecks.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DagAir.Components.HealthChecks
{
    public static class HealthCheckFeature
    {
        public const string RabbitMqHealthCheck = "rabbitMq";
        public const string ReadyHealthCheck = "readyHealthCheck";

        public static IServiceCollection AddDagAirHealthChecks(this IServiceCollection services)
        {
            services.AddSingleton<IGlobalHealthCheckFlags, GlobalHealthCheckFlags>();
            services.AddHostedService<BackGroundHealthCheckService>();

            
            
            services.AddHealthChecks()
                .AddCheck<RabbitMqHealthCheck>(RabbitMqHealthCheck,
                    HealthStatus.Unhealthy,
                    new[] {HealthCheckTags.Internal.ToString()});

            return services;
        }
    }
}