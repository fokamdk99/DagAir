using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DagAir.Components.HealthChecks
{
    public static class HealthCheckExtensions
    {
        public static void MapHealthCheckEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/system/health/live", new HealthCheckOptions
            {
                Predicate = (check) => check.Tags.Contains(HealthCheckTags.Live.ToString()),
                AllowCachingResponses = false,
                ResponseWriter = (context, report) => Task.CompletedTask,
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status204NoContent,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                }
            });
            
            endpoints.MapHealthChecks("/system/health/ready", new HealthCheckOptions
            {
                Predicate = (check) => check.Tags.Contains(HealthCheckTags.Ready.ToString()),
                AllowCachingResponses = false,
                ResponseWriter = (context, report) => Task.CompletedTask,
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status204NoContent,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                }
            });
        }
    }
}