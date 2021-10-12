using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DagAir.Components.HealthChecks.HealthChecks
{
    public class RabbitMqHealthCheck : IHealthCheck
    {
        private readonly IBusControl _busControl;

        public RabbitMqHealthCheck(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var busHealthResult = _busControl.CheckHealth();

            return Task.FromResult(new HealthCheckResult(MapStatus(busHealthResult.Status), busHealthResult.Description, busHealthResult.Exception));
        }

        private static HealthStatus MapStatus(BusHealthStatus busStatus)
        {
            return busStatus switch
            {
                BusHealthStatus.Unhealthy => HealthStatus.Unhealthy,
                BusHealthStatus.Degraded => HealthStatus.Degraded,
                BusHealthStatus.Healthy => HealthStatus.Healthy,
                _ => throw new ArgumentOutOfRangeException(nameof(busStatus), busStatus, null)
            };
        }
    }
}