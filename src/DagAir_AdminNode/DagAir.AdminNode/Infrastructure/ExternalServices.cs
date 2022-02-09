using Microsoft.Extensions.Configuration;

namespace DagAir.AdminNode.Infrastructure
{
    public class ExternalServices : IExternalServices
    {
        public string FacilitiesApi { get; set; }
        public string AddressesApi { get; set; }
        public string SensorStateHistory { get; set; }
        public string SensorState { get; set; }
        public string SensorsDataService { get; set; }
        public string PoliciesDataService { get; set; }

        public ExternalServices(IConfiguration configuration)
        {
            FacilitiesApi = configuration["serviceUrls:DagAir.Facilities"];
            AddressesApi = configuration["serviceUrls:DagAir.Addresses"];
            SensorStateHistory = configuration["serviceUrls:DagAir.SensorStateHistory"];
            SensorsDataService = configuration["serviceUrls:DagAir.SensorsDataService"];
            PoliciesDataService = configuration["serviceUrls:DagAir.PoliciesDataService"];
            SensorState = configuration["serviceUrls:DagAir.SensorState"];
        }
    }
}