using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DagAir.Components.HttpClients;
using DagAir.Facilities.Contracts.DTOs;
using DagAir.WebAdminApp.Infrastructure;
using DagAir.WebAdminApp.Infrastructure.Facilities;
using Microsoft.Extensions.Logging;

namespace DagAir.WebAdminApp.Affiliates
{
    public class AffiliatesHandler : IAffiliatesHandler
    {
        private readonly DagAirHttpClient _client;
        private readonly IExternalServices _externalServices;
        private readonly ILogger<AffiliatesHandler> _logger;

        public AffiliatesHandler(DagAirHttpClient client, IExternalServices externalServices, ILogger<AffiliatesHandler> logger)
        {
            _client = client;
            _externalServices = externalServices;
            _logger = logger;
        }
        
        public async Task<List<AffiliateDto>> GetAffiliates()
        {
            var path = _externalServices.AdminNode + FacilitiesEndpoints.GetAffiliates;
            var response = await _client.GetAsync<List<AffiliateDto>>(path);

            if (response.Item2 != HttpStatusCode.OK)
            {
                var message =
                    $"Error while trying to get information about affiliates. Status code: ${response.Item2}";
                _logger.LogError(message);
                throw new Exception(message);
            }

            return response.Item1;
        }
        
        public async Task<AffiliateDto> GetAffiliateById(long affiliateId)
        {
            var path = _externalServices.AdminNode + FacilitiesEndpoints.GetAffiliates + affiliateId;
            var response = await _client.GetAsync<AffiliateDto>(path);

            if (response.Item2 != HttpStatusCode.OK)
            {
                var message =
                    $"Error while trying to get information about affiliate with id {affiliateId}. Status code: ${response.Item2}";
                _logger.LogError(message);
                throw new Exception(message);
            }

            return response.Item1;
        }
    }
}