using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DagAir.Components.HealthChecks.HealthChecks
{
    public class ReadyHealthCheck : IHealthCheck
    {
        private readonly IGlobalHealthCheckFlags _globalHealthCheckFlags;

        public ReadyHealthCheck(IGlobalHealthCheckFlags globalHealthCheckFlags)
        {
            _globalHealthCheckFlags = globalHealthCheckFlags;
        }
        
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            if (_globalHealthCheckFlags.IsReady)
            {
                return Task.FromResult(HealthCheckResult.Healthy("All services are ready"));
            }
            
            return Task.FromResult(HealthCheckResult.Unhealthy("Some services are still not ready"));
        }
    }
}