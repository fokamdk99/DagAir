using System;
using System.Net.Http;
using System.Threading.Tasks;
using DagAir.WebAdminApp.Infrastructure;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;

namespace DagAir.WebAdminApp.Identity
{
    public class IdentityHandler : IIdentityHandler
    {
        private readonly HttpClient _client;
        private readonly IExternalServices _externalServices;
        private readonly ILogger<IdentityHandler> _logger;

        public IdentityHandler(HttpClient client, IExternalServices externalServices, ILogger<IdentityHandler> logger)
        {
            _client = client;
            _externalServices = externalServices;
            _logger = logger;
        }

        public async Task<TokenResponse> IssueToken(IdentityResource identityResource)
        {
            var result = await _client.GetDiscoveryDocumentAsync(_externalServices.IdentityServer);
            if (result.IsError)
            {
                var message = result.Error;
                _logger.LogError(message);
                throw new Exception(message);
            }

            var tokenResponse = await _client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = result.TokenEndpoint,
                ClientId = identityResource.ClientId,
                ClientSecret = identityResource.ClientSecret,
                Scope = identityResource.Scope
            });

            if (tokenResponse.IsError)
            {
                var message = tokenResponse.Error;
                _logger.LogError(message);
                throw new Exception(message);
            }

            return tokenResponse;
        }
    }
}