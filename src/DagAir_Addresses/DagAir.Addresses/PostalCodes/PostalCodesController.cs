using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DagAir.Addresses.Contracts.DTOs;
using DagAir.Addresses.Infrastructure.UserApi;
using DagAir.Addresses.PostalCodes.Queries;
using DagAir.Components.ApiModels.Json;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.Addresses.PostalCodes
{
    public class PostalCodesController : AddressesControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGetPostalCodeQuery _getPostalCodeQuery;
        
        public PostalCodesController(IMapper mapper, 
            IGetPostalCodeQuery getPostalCodeQuery)
        {
            _mapper = mapper;
            _getPostalCodeQuery = getPostalCodeQuery;
        }
        
        [HttpGet("postal-codes/{postalCodeId}")]
        [ProducesResponseType(typeof(JsonApiDocument<PostalCodeDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetPostalCode(long postalCodeId)
        {
            var postalCode = await _getPostalCodeQuery.Handle(postalCodeId);
            
            if (postalCode == null)
            {
                return GetPostalCodeDtoNotFoundMessage(postalCodeId);
            }

            PostalCodeDto postalCodeDto = _mapper.Map<PostalCodeDto>(postalCode);
            
            return Ok(new JsonApiDocument<PostalCodeDto>(postalCodeDto));
        }
        
        private NotFoundObjectResult GetPostalCodeDtoNotFoundMessage(long roomId)
        {
            return NotFound($"No postal code with Id: {roomId} has been found");
        }
    }
}