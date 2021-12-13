using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DagAir.AdminNode.Infrastructure.UserApi;
using DagAir.Components.ApiModels.Json;
using DagAir.Facilities.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.AdminNode.Facilities
{
    public class FacilitiesAdminController : AdminControllerBase
    {
        private readonly IFacilitiesHandler _facilitiesHandler;

        public FacilitiesAdminController(IFacilitiesHandler facilitiesHandler)
        {
            _facilitiesHandler = facilitiesHandler;
        }
        
        /// <summary>
        /// Returns information about all organizations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("organizations")]
        [ProducesResponseType(typeof(JsonApiDocument<List<OrganizationDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOrganizations()
        {
            var organizationDtos = await _facilitiesHandler.GetOrganizations();

            return Ok(new JsonApiDocument<List<OrganizationDto>>(organizationDtos));
        }
        
        /// <summary>
        /// Returns information about an organization with a given organizationId
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("organizations/{organizationId}")]
        [ProducesResponseType(typeof(JsonApiDocument<OrganizationDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOrganizationById(long organizationId)
        {
            var organizationDtos = await _facilitiesHandler.GetOrganizationById(organizationId);

            return Ok(new JsonApiDocument<OrganizationDto>(organizationDtos));
        }
    }
}