using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DagAir.AdminNode.Infrastructure;
using DagAir.AdminNode.Infrastructure.Facilities;
using DagAir.Components.HttpClients;
using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Contracts.DTOs;
using Microsoft.Extensions.Logging;

namespace DagAir.AdminNode.Facilities
{
    public class FacilitiesHandler : IFacilitiesHandler
    {
        private readonly DagAirHttpClient _client;
        private readonly IExternalServices _externalServices;
        private readonly ILogger<FacilitiesHandler> _logger;

        public FacilitiesHandler(DagAirHttpClient client, 
            IExternalServices externalServices, 
            ILogger<FacilitiesHandler> logger)
        {
            _client = client;
            _externalServices = externalServices;
            _logger = logger;
        }

        public async Task<List<OrganizationDto>> GetOrganizations()
        {
            var path = _externalServices.FacilitiesApi + FacilitiesEndpoints.GetOrganizations;
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
        
        public async Task<OrganizationDto> GetOrganizationById(long organizationId)
        {
            var path = _externalServices.FacilitiesApi + FacilitiesEndpoints.GetOrganizations + organizationId;
            var response = await _client.GetAsync<OrganizationDto>(path);

            if (response.Item2 != HttpStatusCode.OK)
            {
                var message =
                    $"Error while trying to get information about organizations. Status code: ${response.Item2}";
                _logger.LogError(message);
                throw new Exception(message);
            }

            return response.Item1;
        }
        
        public async Task<OrganizationDto> AddNewOrganization(AddNewOrganizationCommand addNewAddressCommand)
        {
            var path = _externalServices.FacilitiesApi + FacilitiesEndpoints.GetOrganizations;
            (var newOrganization, var statusCode) = await _client.PostAsync<AddNewOrganizationCommand, OrganizationDto>(path, addNewAddressCommand);

            if (statusCode == HttpStatusCode.Conflict)
            {
                return null;
            }
            
            if (statusCode != HttpStatusCode.Created)
            {
                var message =
                    $"Error while trying to add new address. Status code: ${statusCode}. AddNewAddressCommand: {addNewAddressCommand}";
                _logger.LogError(message);
                throw new Exception(message);
            }
            
            return newOrganization;
        }
    }
}