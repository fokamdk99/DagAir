using System.Linq;
using System.Threading.Tasks;
using DagAir.IngestionNode.Data.Influx;
using InfluxDB.Client;

namespace DagAir.IngestionNode.Tests.Influx
{
    public static class InfluxHelper
    {

        public static async Task<string> GetOrganizationIdByOrganizationName(InfluxDBClient client, IInfluxConfiguration influxConfiguration)
        {
            var organizations = await client.GetOrganizationsApi().FindOrganizationsAsync();
            var organizationId = organizations.Single(x => x.Name == influxConfiguration.Org).Id;
            return organizationId;
        }
    }
}