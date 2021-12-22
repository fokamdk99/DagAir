using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DagAir.Addresses.Addresses.Queries;
using DagAir.Addresses.Contracts.Commands;
using DagAir.Addresses.Contracts.DTOs;
using DagAir.Addresses.Data.AppEntities;
using DagAir.Addresses.Infrastructure.UserApi;
using DagAir.Components.ApiModels.Json;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.Addresses.Addresses
{
    public class AddressesController : AddressesControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGetAddressQuery _getAddressQuery;
        private readonly ICommandHandler<AddNewAddressCommand, Address> _addNewAddressCommandHandler;
        
        public AddressesController(IMapper mapper, 
            IGetAddressQuery getAddressQuery, 
            ICommandHandler<AddNewAddressCommand, Address> addNewAddressCommandHandler)
        {
            _mapper = mapper;
            _getAddressQuery = getAddressQuery;
            _addNewAddressCommandHandler = addNewAddressCommandHandler;
        }
        
        /// <summary>
        /// Returns information about an address with a given addressId
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
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
        
        [HttpPost("addresses")]
        [ProducesResponseType(typeof(JsonApiDocument<AddressDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAddress([FromBody] AddNewAddressCommand addNewAddressCommand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var address = await _addNewAddressCommandHandler.Handle(addNewAddressCommand);

            AddressDto addressDto = _mapper.Map<AddressDto>(address);
            
            return Created(new JsonApiDocument<AddressDto>(addressDto));
        }
        
        private NotFoundObjectResult GetAddressNotFoundMessage(long roomId)
        {
            return NotFound($"No current room policy for room with Id: {roomId} has not been found");
        }
    }
}