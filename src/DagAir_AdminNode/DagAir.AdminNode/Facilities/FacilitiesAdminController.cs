using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DagAir.AdminNode.Addresses;
using DagAir.AdminNode.Contracts.DTOs;
using DagAir.AdminNode.Infrastructure.UserApi;
using DagAir.Components.ApiModels.Json;
using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.AdminNode.Facilities
{
    public class FacilitiesAdminController : AdminControllerBase
    {
        private readonly IFacilitiesHandler _facilitiesHandler;
        private readonly IAddressesHandler _addressesHandler;

        public FacilitiesAdminController(IFacilitiesHandler facilitiesHandler, IAddressesHandler addressesHandler)
        {
            _facilitiesHandler = facilitiesHandler;
            _addressesHandler = addressesHandler;
        }
        
        /// <summary>
        /// Returns information about all organizations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("organizations")]
        [ProducesResponseType(typeof(JsonApiDocument<List<AdminNodeOrganizationDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOrganizations()
        {
            var organizationDtos = await _facilitiesHandler.GetOrganizations();
            var addressDtosTasks = organizationDtos.Select(async x => 
                await _addressesHandler.GetAddressById(x.AddressId));

            var addressDtos = await Task.WhenAll(addressDtosTasks);

            var organizationsWithAddresses = organizationDtos.Zip(addressDtos,
                (o, a) => new AdminNodeOrganizationDto {OrganizationDto = o, AddressDto = a}).ToList();

            return Ok(new JsonApiDocument<List<AdminNodeOrganizationDto>>(organizationsWithAddresses));
        }
        
        /// <summary>
        /// Returns information about an organization with a given organizationId
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("organizations/{organizationId}")]
        [ProducesResponseType(typeof(JsonApiDocument<AdminNodeOrganizationDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOrganizationById(long organizationId)
        {
            var organizationDto = await _facilitiesHandler.GetOrganizationById(organizationId);
            var addressDto = await _addressesHandler.GetAddressById(organizationDto.AddressId);
            var adminNodeOrganizationDto = new AdminNodeOrganizationDto
            {
                OrganizationDto = organizationDto,
                AddressDto = addressDto
            };

            return Ok(new JsonApiDocument<AdminNodeOrganizationDto>(adminNodeOrganizationDto));
        }
        
        [HttpPost]
        [Route("organizations")]
        [ProducesResponseType(typeof(JsonApiDocument<OrganizationDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddNewAddress([FromBody] AddNewOrganizationCommand addNewAddressCommand)
        {
            if (ModelState.IsValid)
            {
                var newOrganization = await _facilitiesHandler.AddNewOrganization(addNewAddressCommand);
                return Created(new JsonApiDocument<OrganizationDto>(newOrganization));
            }

            return BadRequest();
        }
    }
}