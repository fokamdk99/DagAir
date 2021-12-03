using Microsoft.Extensions.Configuration;

namespace DagAir.PolicyNode.Integrations
{
    public class ServicesUrls : IServicesUrls
    {
        public string PoliciesApi { get; set; }
        public string FacilitiesApi { get; set; }
        
        private const string ConfigurationSection = "serviceUrls"; 
        
        public ServicesUrls(IConfiguration configuration)
        {
            PoliciesApi = configuration.GetSection($"{ConfigurationSection}:DagAir.Policies").Value;
            FacilitiesApi = configuration.GetSection($"{ConfigurationSection}:DagAir.Facilities").Value;
        }
    }
}