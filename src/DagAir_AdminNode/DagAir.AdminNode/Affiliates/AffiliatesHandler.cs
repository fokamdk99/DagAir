using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DagAir.AdminNode.Facilities;
using DagAir.AdminNode.Infrastructure;
using DagAir.AdminNode.Infrastructure.Facilities;
using DagAir.Components.HttpClients;
using DagAir.Facilities.Contracts.DTOs;
using Microsoft.Extensions.Logging;

namespace DagAir.AdminNode.Affiliates
{
    public class AffiliatesHandler : IAffiliatesHandler
    {
        private readonly DagAirHttpClient _client;
        private readonly IExternalServices _externalServices;
        private readonly ILogger<FacilitiesHandler> _logger;

        public AffiliatesHandler(DagAirHttpClient client, 
            IExternalServices externalServices, 
            ILogger<FacilitiesHandler> logger)
        {
            _client = client;
            _externalServices = externalServices;
            _logger = logger;
        }
        
        public async Task<List<AffiliateDto>> GetAffiliates()
        {
            var path = _externalServices.FacilitiesApi + FacilitiesEndpoints.GetAffiliates;
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
            var path = _externalServices.FacilitiesApi + FacilitiesEndpoints.GetAffiliates + affiliateId;
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