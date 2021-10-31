using System.Collections.Generic;
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
        public const string LiveHealthCheck = "liveHealthCheck";

        public static IServiceCollection AddDagAirHealthChecks(this IServiceCollection services, List<string> healthCheckToBeDisabled)
        {
            services.AddSingleton<IGlobalHealthCheckFlags, GlobalHealthCheckFlags>();
            services.AddHostedService<BackGroundHealthCheckService>();

            if (!healthCheckToBeDisabled.Contains(RabbitMqHealthCheck))
            {
                services.AddHealthChecks()
                    .AddCheck<RabbitMqHealthCheck>(RabbitMqHealthCheck,
                        HealthStatus.Unhealthy,
                        new[] {HealthCheckTags.Internal.ToString()});
            }

            services.AddHealthChecks()
                .AddCheck<ReadyHealthCheck>(ReadyHealthCheck,
                    HealthStatus.Unhealthy,
                    new[] {HealthCheckTags.ReadyFastLane.ToString()});
            
            services.AddHealthChecks()
                .AddCheck<LiveHealthCheck>(LiveHealthCheck,
                    HealthStatus.Unhealthy,
                    new[] {HealthCheckTags.Live.ToString()});
            
            return services;
        }
    }
}