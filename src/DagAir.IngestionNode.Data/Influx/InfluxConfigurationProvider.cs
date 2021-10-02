using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DagAir.IngestionNode.Data.Influx
{
    public class InfluxConfigurationProvider : IInfluxConfigurationProvider
    {

        private const string InfluxConfigurationSection = "DagAirInfluxConfiguration";
        
        private readonly ILogger<InfluxConfigurationProvider> _logger;

        private readonly InfluxConfiguration _influxConfiguration;
        
        public InfluxConfigurationProvider(IConfiguration configuration, ILogger<InfluxConfigurationProvider> _logger)
        {
            _logger = this._logger;

            var influxConfigurationSection = configuration.GetSection(InfluxConfigurationSection);
            if (influxConfigurationSection.Exists())
            {
                _influxConfiguration = LoadInfluxConfiguration(configuration);
            }
        }

        public InfluxConfiguration Provide()
        {
            return _influxConfiguration;
        }

        private InfluxConfiguration LoadInfluxConfiguration(IConfiguration cfg)
        {
            return new InfluxConfiguration(
                cfg[InfluxConfigurationSection + ":token"].ToCharArray(),
                cfg[InfluxConfigurationSection + ":org"],
                cfg[InfluxConfigurationSection + ":orgId"],
                cfg[InfluxConfigurationSection + ":bucketName"],
                cfg[InfluxConfigurationSection + ":url"],
                int.Parse(cfg[InfluxConfigurationSection + ":retention"])
            );
        }
    }
}