namespace DagAir.WebAdminApp.Infrastructure
{
    public interface IExternalServices
    {
        string FacilitiesApi { get; set; }
        string IdentityServer { get; set; }
        string AdminNode { get; set; }
    }
}