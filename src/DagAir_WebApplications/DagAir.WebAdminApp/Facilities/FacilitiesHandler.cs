using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DagAir.Addresses.Contracts.Commands;
using DagAir.Addresses.Contracts.DTOs;
using DagAir.AdminNode.Contracts.DTOs;
using DagAir.Components.HttpClients;
using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Contracts.DTOs;
using DagAir.WebAdminApp.Controllers;
using DagAir.WebAdminApp.Infrastructure;
using DagAir.WebAdminApp.Infrastructure.Addresses;
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

        public async Task<List<AdminNodeOrganizationDto>> GetOrganizations()
        {
            var path = _externalServices.AdminNode + FacilitiesEndpoints.GetOrganizations;
            var response = await _client.GetAsync<List<AdminNodeOrganizationDto>>(path);

            if (response.Item2 != HttpStatusCode.OK)
            {
                var message =
                    $"Error while trying to get information about organizations. Status code: ${response.Item2}";
                _logger.LogError(message);
                throw new Exception(message);
            }

            return response.Item1;
        }
        
        public async Task<AdminNodeOrganizationDto> GetOrganization(long organizationId)
        {
            var path = _externalServices.AdminNode + FacilitiesEndpoints.GetOrganizations + organizationId;
            var response = await _client.GetAsync<AdminNodeOrganizationDto>(path);

            if (response.Item2 != HttpStatusCode.OK)
            {
                var message =
                    $"Error while trying to get information about organization with id {organizationId}. Status code: ${response.Item2}";
                _logger.LogError(message);
                throw new Exception(message);
            }

            return response.Item1;
        }

        public async Task<OrganizationDto> AddNewOrganization(GetOrganizationModel getOrganizationModel)
        {
            var cityDto = new CityDto {Name = getOrganizationModel.AdminNodeOrganizationDto.AddressDto.City.Name};
            var countryDto = new CountryDto {Name = getOrganizationModel.AdminNodeOrganizationDto.AddressDto.Country.Name};
            var postalCodeDto = new PostalCodeDto {Number = getOrganizationModel.AdminNodeOrganizationDto.AddressDto.PostalCode.Number};
            var addressDto = new AddressDto {};
            
            var addNewAddressCommand = new AddNewAddressCommand();
            addNewAddressCommand.AddressDto = addressDto;
            addNewAddressCommand.CityDto = cityDto;
            addNewAddressCommand.CountryDto = countryDto;
            addNewAddressCommand.PostalCodeDto = postalCodeDto;
            
            var addressPath = _externalServices.AdminNode + AddressesEndpoints.GetAddresses;
            (var newAddress, var addressStatusCode) = await _client.PostAsync<AddNewAddressCommand, AddressDto>(addressPath, addNewAddressCommand);

            if (addressStatusCode != HttpStatusCode.Created)
            {
                var message =
                    $"Error while trying to add new address. Status code: ${addressStatusCode}. AddNewAddressCommand: {addNewAddressCommand}";
                _logger.LogError(message);
                throw new Exception(message);
            }

            var organizationPath = _externalServices.AdminNode + FacilitiesEndpoints.GetOrganizations;

            var addNewOrganizationCommand = new AddNewOrganizationCommand { OrganizationDto = new OrganizationDto() };
            addNewOrganizationCommand.OrganizationDto.Name =
                getOrganizationModel.AdminNodeOrganizationDto.OrganizationDto.Name;
            addNewOrganizationCommand.OrganizationDto.AddressId = newAddress.Id;
            
            (var newOrganization, var organizationStatusCode) = await _client.PostAsync<AddNewOrganizationCommand, OrganizationDto>(organizationPath, addNewOrganizationCommand);

            if (organizationStatusCode == HttpStatusCode.Conflict)
            {
                return null;
            }
            
            if (organizationStatusCode != HttpStatusCode.Created)
            {
                var message =
                    $"Error while trying to add new organization. Status code: ${organizationStatusCode}. AddNewOrganizationCommand: {addNewOrganizationCommand}";
                _logger.LogError(message);
                throw new Exception(message);
            }

            return newOrganization;
        }
    }
}