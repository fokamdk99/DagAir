namespace DagAir.WebAdminApp.Infrastructure
{
    public interface IExternalServices
    {
        string IdentityServer { get; set; }
        string AdminNode { get; set; }
    }
}