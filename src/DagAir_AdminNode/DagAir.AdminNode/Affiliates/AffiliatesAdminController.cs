using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DagAir.AdminNode.Infrastructure.UserApi;
using DagAir.Components.ApiModels.Json;
using DagAir.Facilities.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.AdminNode.Affiliates
{
    public class AffiliatesAdminController : AdminControllerBase
    {
        private readonly IAffiliatesHandler _affiliatesHandler;

        public AffiliatesAdminController(IAffiliatesHandler affiliatesHandler)
        {
            _affiliatesHandler = affiliatesHandler;
        }
        
        /// <summary>
        /// Returns information about all affiliates
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("affiliates")]
        [ProducesResponseType(typeof(JsonApiDocument<List<AffiliateDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAffiliates()
        {
            var affiliateDtos = await _affiliatesHandler.GetAffiliates();

            return Ok(new JsonApiDocument<List<AffiliateDto>>(affiliateDtos));
        }
        
        /// <summary>
        /// Returns information about an affiliate with a given affiliateId
        /// </summary>
        /// <param name="affiliateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("affiliates/{affiliateId}")]
        [ProducesResponseType(typeof(JsonApiDocument<AffiliateDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOrganizationById(long affiliateId)
        {
            var affiliateDto = await _affiliatesHandler.GetAffiliateById(affiliateId);

            return Ok(new JsonApiDocument<AffiliateDto>(affiliateDto));
        }
    }
}