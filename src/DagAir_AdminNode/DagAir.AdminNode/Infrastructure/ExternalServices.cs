using Microsoft.Extensions.Configuration;

namespace DagAir.AdminNode.Infrastructure
{
    public class ExternalServices : IExternalServices
    {
        public string FacilitiesApi { get; set; }
        public string AddressesApi { get; set; }

        public ExternalServices(IConfiguration configuration)
        {
            FacilitiesApi = configuration["serviceUrls:DagAir.Facilities"];
            AddressesApi = configuration["serviceUrls:DagAir.Addresses"];
        }
    }
}