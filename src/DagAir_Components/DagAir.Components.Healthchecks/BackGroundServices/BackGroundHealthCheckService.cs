using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DagAir.Components.HealthChecks.BackGroundServices
{
    public class BackGroundHealthCheckService : BackgroundService
    {
        private const int Frequency = 10000;
        private readonly HealthCheckService _healthCheckService;
        private readonly IGlobalHealthCheckFlags _globalHealthCheckFlags;
        private readonly ILogger<BackGroundHealthCheckService> _logger;

        public BackGroundHealthCheckService(ILogger<BackGroundHealthCheckService> logger,
            HealthCheckService healthCheckService,
            IGlobalHealthCheckFlags globalHealthCheckFlags)
        {
            _logger = logger;
            _healthCheckService = healthCheckService;
            _globalHealthCheckFlags = globalHealthCheckFlags;
        }

        public async Task CheckHealth(CancellationToken cancellationToken)
        {
            try
            {
                var healthReport = await _healthCheckService.CheckHealthAsync(
                    x => !x.Tags.Contains(HealthCheckTags.ReadyFastLane.ToString()), cancellationToken);

                var healthReportEntries = healthReport.Entries;

                _globalHealthCheckFlags.IsReady = healthReportEntries.All(n => n.Value.Status == HealthStatus.Healthy);

                _logger.LogInformation($"IsReady flag: {_globalHealthCheckFlags.IsReady}");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(Frequency, cancellationToken);
                await CheckHealth(cancellationToken);
            }

            await StopAsync(cancellationToken);
        }
    }
}