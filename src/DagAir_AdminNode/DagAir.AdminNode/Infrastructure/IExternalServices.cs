namespace DagAir.AdminNode.Infrastructure
{
    public interface IExternalServices
    {
        string FacilitiesApi { get; set; }
        string AddressesApi { get; set; }
        string SensorStateHistory { get; set; }
        string SensorState { get; set; }
        string SensorsDataService { get; set; }
        public string PoliciesDataService { get; set; }
    }
}