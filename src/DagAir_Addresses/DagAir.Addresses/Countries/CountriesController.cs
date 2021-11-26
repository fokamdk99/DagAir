using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DagAir.Addresses.Contracts.DTOs;
using DagAir.Addresses.Countries.Queries;
using DagAir.Addresses.Infrastructure.UserApi;
using DagAir.Components.ApiModels.Json;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.Addresses.Countries
{
    public class CountriesController : AddressesControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGetCountryQuery _getCountryQuery;
        
        public CountriesController(IMapper mapper, 
            IGetCountryQuery getCityQuery)
        {
            _mapper = mapper;
            _getCountryQuery = getCityQuery;
        }
        
        [HttpGet("countries/{countryId}")]
        [ProducesResponseType(typeof(JsonApiDocument<CountryDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCountry(long countryId)
        {
            var country = await _getCountryQuery.Handle(countryId);
            
            if (country == null)
            {
                return GetCountryNotFoundMessage(countryId);
            }

            CountryDto countryDto = _mapper.Map<CountryDto>(country);
            
            return Ok(new JsonApiDocument<CountryDto>(countryDto));
        }
        
        private NotFoundObjectResult GetCountryNotFoundMessage(long roomId)
        {
            return NotFound($"No country with Id: {roomId} has been found");
        }
    }
}