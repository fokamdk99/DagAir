using System.Linq;
using System.Threading.Tasks;
using InfluxDB.Client;
using Microsoft.Extensions.Logging;

namespace DagAir.Components.Influx
{
    public class InfluxHelper : IInfluxHelper
    {
        private readonly ILogger<InfluxHelper> _logger;

        public InfluxHelper(ILogger<InfluxHelper> logger)
        {
            _logger = logger;
        }

        public async Task<string> GetOrganizationIdByOrganizationName(InfluxDBClient client, IInfluxConfiguration influxConfiguration)
        {
            _logger.LogInformation($"URL: {influxConfiguration.Url}");
            var organizations = await client.GetOrganizationsApi().FindOrganizationsAsync();
            var organizationId = organizations.Single(x => x.Name == influxConfiguration.Org).Id;
            return organizationId;
        }
    }
}