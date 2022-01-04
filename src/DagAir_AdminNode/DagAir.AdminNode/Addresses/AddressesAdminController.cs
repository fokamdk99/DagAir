using System.Net;
using System.Threading.Tasks;
using DagAir.Addresses.Contracts.Commands;
using DagAir.Addresses.Contracts.DTOs;
using DagAir.AdminNode.Infrastructure.UserApi;
using DagAir.Components.ApiModels.Json;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.AdminNode.Addresses
{
    public class AddressesAdminController : AdminControllerBase
    {
        private readonly IAddressesHandler _addressesHandler;

        public AddressesAdminController(IAddressesHandler addressesHandler)
        {
            _addressesHandler = addressesHandler;
        }

        [HttpPost]
        [Route("addresses")]
        [ProducesResponseType(typeof(JsonApiDocument<AddressDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddNewAddress([FromBody] AddNewAddressCommand addNewAddressCommand)
        {
            if (ModelState.IsValid)
            {
                var newAddress = await _addressesHandler.AddNewAddress(addNewAddressCommand);
                return Created(new JsonApiDocument<AddressDto>(newAddress));
            }

            return BadRequest();
        }
    }
}