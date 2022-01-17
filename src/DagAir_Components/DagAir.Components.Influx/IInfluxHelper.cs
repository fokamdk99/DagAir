using System.Threading.Tasks;
using InfluxDB.Client;

namespace DagAir.Components.Influx
{
    public interface IInfluxHelper
    {
        Task<string> GetOrganizationIdByOrganizationName(InfluxDBClient client,
            IInfluxConfiguration influxConfiguration);
    }
}