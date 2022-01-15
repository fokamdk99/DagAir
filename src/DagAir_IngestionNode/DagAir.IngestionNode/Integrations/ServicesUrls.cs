using Microsoft.Extensions.Configuration;

namespace DagAir.IngestionNode.Integrations
{
    public class ServicesUrls : IServicesUrls
    {
        public string SensorStateHistory { get; set; }
        
        private const string ConfigurationSection = "serviceUrls"; 
        
        public ServicesUrls(IConfiguration configuration)
        {
            SensorStateHistory = configuration.GetSection($"{ConfigurationSection}:DagAir.SensorStateHistory").Value;
        }
    }
}