using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DagAir.AdminNode.Addresses;
using DagAir.AdminNode.Contracts.DTOs;
using DagAir.AdminNode.Infrastructure.UserApi;
using DagAir.Components.ApiModels.Json;
using DagAir.Facilities.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.AdminNode.Affiliates
{
    public class AffiliatesAdminController : AdminControllerBase
    {
        private readonly IAffiliatesHandler _affiliatesHandler;
        private readonly IAddressesHandler _addressesHandler;

        public AffiliatesAdminController(IAffiliatesHandler affiliatesHandler, 
            IAddressesHandler addressesHandler)
        {
            _affiliatesHandler = affiliatesHandler;
            _addressesHandler = addressesHandler;
        }
        
        /// <summary>
        /// Returns information about all affiliates
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("affiliates")]
        [ProducesResponseType(typeof(JsonApiDocument<List<AdminNodeAffiliateDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAffiliates()
        {
            var affiliateDtos = await _affiliatesHandler.GetAffiliates();
            var addressDtosTasks = affiliateDtos.Select(async x => 
                await _addressesHandler.GetAddressById(x.AddressId));

            var addressDtos = await Task.WhenAll(addressDtosTasks);

            var affiliatesWithAddresses = affiliateDtos.Zip(addressDtos,
                (a, ad)=> new AdminNodeAffiliateDto {AffiliateDto = a, AddressDto = ad}).ToList();

            return Ok(new JsonApiDocument<List<AdminNodeAffiliateDto>>(affiliatesWithAddresses));
        }
        
        /// <summary>
        /// Returns information about an affiliate with a given affiliateId
        /// </summary>
        /// <param name="affiliateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("affiliates/{affiliateId}")]
        [ProducesResponseType(typeof(JsonApiDocument<AdminNodeAffiliateDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOrganizationById(long affiliateId)
        {
            var affiliateDto = await _affiliatesHandler.GetAffiliateById(affiliateId);
            var addressDto = await _addressesHandler.GetAddressById(affiliateDto.AddressId);
            var adminNodeOrganizationDto = new AdminNodeAffiliateDto
            {
                AffiliateDto = affiliateDto,
                AddressDto = addressDto
            };

            return Ok(new JsonApiDocument<AdminNodeAffiliateDto>(adminNodeOrganizationDto));
        }
    }
}