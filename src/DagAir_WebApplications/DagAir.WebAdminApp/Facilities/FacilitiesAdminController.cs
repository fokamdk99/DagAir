using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DagAir.AdminNode.Contracts.DTOs;
using DagAir.Components.ApiModels.Json;
using DagAir.Facilities.Contracts.DTOs;
using DagAir.WebAdminApp.Infrastructure.UserApi;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.WebAdminApp.Facilities
{
    public class FacilitiesAdminController : AdminControllerBase
    {
        private readonly IFacilitiesHandler _facilitiesHandler;

        public FacilitiesAdminController(IFacilitiesHandler facilitiesHandler)
        {
            _facilitiesHandler = facilitiesHandler;
        }
        
        [HttpGet]
        [Route("organizations")]
        [ProducesResponseType(typeof(JsonApiDocument<List<AdminNodeOrganizationDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOrganizations()
        {
            var organizationDtos = await _facilitiesHandler.GetOrganizations();

            return Ok(new JsonApiDocument<List<AdminNodeOrganizationDto>>(organizationDtos));
        }
    }
}