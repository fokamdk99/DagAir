using System.Threading.Tasks;
using IdentityModel.Client;

namespace DagAir.WebApps.WebAdminApp2.Identity
{
    public interface IIdentityHandler
    {
        Task<TokenResponse> IssueToken(IdentityResource identityResource);
    }
}