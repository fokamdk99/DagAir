using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DagAir.Components.HttpClients;
using DagAir.Facilities.Contracts.DTOs;
using DagAir.WebAdminApp.Identity;
using DagAir.WebAdminApp.Infrastructure;
using DagAir.WebAdminApp.Infrastructure.Facilities;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;

namespace DagAir.WebAdminApp.Facilities
{
    public class FacilitiesHandler : IFacilitiesHandler
    {
        private readonly DagAirHttpClient _client;
        private readonly IExternalServices _externalServices;
        private readonly ILogger<FacilitiesHandler> _logger;
        private readonly IIdentityHandler _identityHandler;

        public FacilitiesHandler(DagAirHttpClient client, IExternalServices externalServices, ILogger<FacilitiesHandler> logger, IIdentityHandler identityHandler)
        {
            _client = client;
            _externalServices = externalServices;
            _logger = logger;
            _identityHandler = identityHandler;
        }

        public async Task<List<OrganizationDto>> GetOrganizations()
        {
            var identityResource = new IdentityResource
            {
                ClientId = "WebAdminApp",
                ClientSecret = "secret",
                Scope = "DagAir.Facilities"
            };
            
            var identityToken = await _identityHandler.IssueToken(identityResource);
            var client = new HttpClient();
            client.SetBearerToken(identityToken.AccessToken);
            _client.ConfigureClient(client);
            var path = _externalServices.AdminNode + FacilitiesEndpoints.GetOrganizations;
            var response = await _client.GetAsync<List<OrganizationDto>>(path);

            if (response.Item2 != HttpStatusCode.OK)
            {
                var message =
                    $"Error while trying to get information about organizations. Status code: ${response.Item2}";
                _logger.LogError(message);
                throw new Exception(message);
            }

            return response.Item1;
        }
        
        public async Task<OrganizationDto> GetOrganization(long organizationId)
        {
            var identityResource = new IdentityResource
            {
                ClientId = "WebAdminApp",
                ClientSecret = "secret",
                Scope = "DagAir.Facilities"
            };
            
            var identityToken = await _identityHandler.IssueToken(identityResource);
            var client = new HttpClient();
            client.SetBearerToken(identityToken.AccessToken);
            _client.ConfigureClient(client);
            var path = _externalServices.AdminNode + FacilitiesEndpoints.GetOrganizations + organizationId;
            var response = await _client.GetAsync<OrganizationDto>(path);

            if (response.Item2 != HttpStatusCode.OK)
            {
                var message =
                    $"Error while trying to get information about organization with id {organizationId}. Status code: ${response.Item2}";
                _logger.LogError(message);
                throw new Exception(message);
            }

            return response.Item1;
        }
    }
}