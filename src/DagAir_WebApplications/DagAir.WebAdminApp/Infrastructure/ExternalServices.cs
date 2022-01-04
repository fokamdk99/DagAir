using Microsoft.Extensions.Configuration;

namespace DagAir.WebAdminApp.Infrastructure
{
    public class ExternalServices : IExternalServices
    {
        public string IdentityServer { get; set; }
        public string AdminNode { get; set; }

        public ExternalServices(IConfiguration configuration)
        {
            IdentityServer = configuration["serviceUrls:DagAir.IdentityServer"];
            AdminNode = configuration["serviceUrls:DagAir.AdminNode"];
        }
    }
}