using System.Linq;
using System.Threading.Tasks;
using InfluxDB.Client;

namespace DagAir.Components.Influx
{
    public class InfluxHelper : IInfluxHelper
    {

        public async Task<string> GetOrganizationIdByOrganizationName(InfluxDBClient client, IInfluxConfiguration influxConfiguration)
        {
            var organizations = await client.GetOrganizationsApi().FindOrganizationsAsync();
            var organizationId = organizations.Single(x => x.Name == influxConfiguration.Org).Id;
            return organizationId;
        }
    }
}