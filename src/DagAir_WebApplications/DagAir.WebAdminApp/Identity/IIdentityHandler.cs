using System.Threading.Tasks;
using IdentityModel.Client;

namespace DagAir.WebAdminApp.Identity
{
    public interface IIdentityHandler
    {
        Task<TokenResponse> IssueToken(IdentityResource identityResource);
    }
}