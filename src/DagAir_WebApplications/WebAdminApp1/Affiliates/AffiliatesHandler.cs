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
using Microsoft.Extensions.Logging;
using WebAdminApp1.Controllers;
using WebAdminApp1.Infrastructure;
using WebAdminApp1.Infrastructure.Addresses;
using WebAdminApp1.Infrastructure.Facilities;

namespace WebAdminApp1.Affiliates
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
        
        public async Task<List<AdminNodeAffiliateDto>> GetAffiliates()
        {
            var path = _externalServices.AdminNode + FacilitiesEndpoints.GetAffiliates;
            var response = await _client.GetAsync<List<AdminNodeAffiliateDto>>(path);

            if (response.Item2 != HttpStatusCode.OK)
            {
                var message =
                    $"Error while trying to get information about affiliates. Status code: ${response.Item2}";
                _logger.LogError(message);
                throw new Exception(message);
            }

            return response.Item1;
        }
        
        public async Task<AdminNodeAffiliateDto> GetAffiliateById(long affiliateId)
        {
            var path = _externalServices.AdminNode + FacilitiesEndpoints.GetAffiliates + affiliateId;
            var response = await _client.GetAsync<AdminNodeAffiliateDto>(path);

            if (response.Item2 != HttpStatusCode.OK)
            {
                var message =
                    $"Error while trying to get information about affiliate with id {affiliateId}. Status code: ${response.Item2}";
                _logger.LogError(message);
                throw new Exception(message);
            }

            return response.Item1;
        }
        
        public async Task<AffiliateDto> AddNewAffiliate(GetAffiliateModel getAffiliateModel)
        {
            var cityDto = new CityDto {Name = getAffiliateModel.AdminNodeAffiliateDto.AddressDto.City.Name};
            var countryDto = new CountryDto {Name = getAffiliateModel.AdminNodeAffiliateDto.AddressDto.Country.Name};
            var postalCodeDto = new PostalCodeDto {Number = getAffiliateModel.AdminNodeAffiliateDto.AddressDto.PostalCode.Number};
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

            var affiliatePath = _externalServices.AdminNode + FacilitiesEndpoints.GetAffiliates;

            var addNewAffiliateCommand = new AddNewAffiliateCommand() { AffiliateDto = new AffiliateDto() };
            addNewAffiliateCommand.AffiliateDto.Name =
                getAffiliateModel.AdminNodeAffiliateDto.AffiliateDto.Name;
            addNewAffiliateCommand.AffiliateDto.AddressId = newAddress.Id;
            addNewAffiliateCommand.AffiliateDto.OrganizationId =
                getAffiliateModel.AdminNodeAffiliateDto.AffiliateDto.OrganizationId;
            
            (var newAffiliate, var affiliateStatusCode) = await _client.PostAsync<AddNewAffiliateCommand, AffiliateDto>(affiliatePath, addNewAffiliateCommand);

            if (affiliateStatusCode == HttpStatusCode.Conflict)
            {
                return null;
            }
            
            if (affiliateStatusCode != HttpStatusCode.Created)
            {
                var message =
                    $"Error while trying to add new affiliate. Status code: ${affiliateStatusCode}. AddNewOrganizationCommand: {addNewAffiliateCommand}";
                _logger.LogError(message);
                throw new Exception(message);
            }

            return newAffiliate;
        }
    }
}