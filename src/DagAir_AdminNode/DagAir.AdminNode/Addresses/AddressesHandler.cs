using System;
using System.Net;
using System.Threading.Tasks;
using DagAir.Addresses.Contracts.Commands;
using DagAir.Addresses.Contracts.DTOs;
using DagAir.AdminNode.Infrastructure;
using DagAir.AdminNode.Infrastructure.Addresses;
using DagAir.Components.HttpClients;
using Microsoft.Extensions.Logging;

namespace DagAir.AdminNode.Addresses
{
    public class AddressesHandler : IAddressesHandler
    {
        private readonly DagAirHttpClient _client;
        private readonly IExternalServices _externalServices;
        private readonly ILogger<AddressesHandler> _logger;

        public AddressesHandler(DagAirHttpClient client, IExternalServices externalServices, ILogger<AddressesHandler> logger)
        {
            _client = client;
            _externalServices = externalServices;
            _logger = logger;
        }

        public async Task<AddressDto> GetAddressById(long addressId)
        {
            var path = _externalServices.AddressesApi + AddressesEndpoints.GetAddresses + addressId;
            var response = await _client.GetAsync<AddressDto>(path);

            if (response.Item2 != HttpStatusCode.OK)
            {
                var message =
                    $"Error while trying to get information about address with id {addressId}. Status code: ${response.Item2}";
                _logger.LogError(message);
                throw new Exception(message);
            }

            return response.Item1;
        }

        public async Task<AddressDto> AddNewAddress(AddNewAddressCommand addNewAddressCommand)
        {
            var path = _externalServices.AddressesApi + AddressesEndpoints.GetAddresses;
            (var newAddress, var statusCode) = await _client.PostAsync<AddNewAddressCommand, AddressDto>(path, addNewAddressCommand);

            if (statusCode != HttpStatusCode.Created)
            {
                var message =
                    $"Error while trying to add new address. Status code: ${statusCode}. AddNewAddressCommand: {addNewAddressCommand}";
                _logger.LogError(message);
                throw new Exception(message);
            }
            
            return newAddress;
        }
    }
}