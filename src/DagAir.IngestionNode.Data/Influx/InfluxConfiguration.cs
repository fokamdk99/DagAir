using Microsoft.Extensions.Configuration;

namespace DagAir.IngestionNode.Data.Influx
{
    public class InfluxConfiguration : IInfluxConfiguration
    {
        public string Token { get; private set; }
        public string Org { get; private set; }
        public string OrgId { get; private set; }
        public string BucketName { get; private set; }
        public string Url { get; private set; }
        public int Retention { get; private set; }

        public static InfluxConfiguration GetConfiguration(IConfiguration configuration, string sectionName)
        {
            var influxConfiguration = new InfluxConfiguration();
            configuration
                .GetSection(sectionName)
                .Bind(influxConfiguration, options => options.BindNonPublicProperties = true);

            return influxConfiguration;
        }
    }
}