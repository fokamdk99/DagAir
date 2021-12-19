using System.Threading.Tasks;
using DagAir.WebAdminApp.Identity;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DagAir.WebAdminApp.Tests.Identity
{
    public class IdentityTests : IntegrationTest
    {
        private IIdentityHandler _identityHandler;

        protected override Task SetupTest()
        {
            _identityHandler = CurrentHost.Services.GetRequiredService<IIdentityHandler>();
            return Task.CompletedTask;
        }
        
        [Ignore("test needs identity server running, to cover in api tests")]
        [Test]
        public async Task IdentityServer_ShouldReturnClientToken()
        {
            var identityResource = new IdentityResource
            {
                ClientId = "WebAdminApp",
                ClientSecret = "secret",
                Scope = "DagAir.Facilities"
            };
            var token = await _identityHandler.IssueToken(identityResource);
            
            Assert.NotNull(token);
        }
        protected override void AddOverrides(IServiceCollection services)
        {
            
        }
    }
}