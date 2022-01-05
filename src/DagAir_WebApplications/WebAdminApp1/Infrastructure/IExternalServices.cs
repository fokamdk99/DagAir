namespace WebAdminApp1.Infrastructure
{
    public interface IExternalServices
    {
        string IdentityServer { get; set; }
        string AdminNode { get; set; }
    }
}