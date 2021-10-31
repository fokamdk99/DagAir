using Microsoft.Extensions.Configuration;

namespace DagAir.IngestionNode.Integrations
{
    public class ServicesUrls : IServicesUrls
    {
        public string SensorsApi { get; set; }
        
        private const string ConfigurationSection = "serviceUrls"; 
        
        public ServicesUrls(IConfiguration configuration)
        {
            SensorsApi = configuration.GetSection($"{ConfigurationSection}:DagAir.Sensors").Value;
        }
    }
}