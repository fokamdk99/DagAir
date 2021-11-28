﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DagAir.Components.HttpClients;
using DagAir.Facilities.Contracts.DTOs;
using DagAir.WebAdminApp.Infrastructure;
using DagAir.WebAdminApp.Infrastructure.Facilities;
using Microsoft.Extensions.Logging;

namespace DagAir.WebAdminApp.Facilities
{
    public class FacilitiesHandler : IFacilitiesHandler
    {
        private readonly DagAirHttpClient _client;
        private readonly IExternalServices _externalServices;
        private readonly ILogger<FacilitiesHandler> _logger;

        public FacilitiesHandler(DagAirHttpClient client, IExternalServices externalServices, ILogger<FacilitiesHandler> logger)
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
    }
}