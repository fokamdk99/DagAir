using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DagAir.Addresses.Addresses.Queries;
using DagAir.Addresses.Contracts.DTOs;
using DagAir.Addresses.Infrastructure.UserApi;
using DagAir.Components.ApiModels.Json;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.Addresses.Addresses
{
    public class AddressesController : AddressesControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGetAddressQuery _getAddressQuery;
        
        public AddressesController(IMapper mapper, 
            IGetAddressQuery getAddressQuery)
        {
            _mapper = mapper;
            _getAddressQuery = getAddressQuery;
        }
        
        [HttpGet("addresses/{addressId}")]
        [ProducesResponseType(typeof(JsonApiDocument<AddressDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAddress(long addressId)
        {
            var address = await _getAddressQuery.Handle(addressId);
            
            if (address == null)
            {
                return GetAddressNotFoundMessage(addressId);
            }

            AddressDto addressDto = _mapper.Map<AddressDto>(address);
            
            return Ok(new JsonApiDocument<AddressDto>(addressDto));
        }
        
        private NotFoundObjectResult GetAddressNotFoundMessage(long roomId)
        {
            return NotFound($"No current room policy for room with Id: {roomId} has not been found");
        }
    }
}