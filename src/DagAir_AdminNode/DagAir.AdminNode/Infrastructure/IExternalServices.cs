namespace DagAir.AdminNode.Infrastructure
{
    public interface IExternalServices
    {
        string FacilitiesApi { get; set; }
        string AddressesApi { get; set; }
    }
}