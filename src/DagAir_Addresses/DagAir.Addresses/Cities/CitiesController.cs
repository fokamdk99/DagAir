using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DagAir.Addresses.Cities.Queries;
using DagAir.Addresses.Contracts.DTOs;
using DagAir.Addresses.Infrastructure.UserApi;
using DagAir.Components.ApiModels.Json;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.Addresses.Cities
{
    public class CitiesController : AddressesControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGetCityQuery _getCityQuery;
        
        public CitiesController(IMapper mapper, 
            IGetCityQuery getCityQuery)
        {
            _mapper = mapper;
            _getCityQuery = getCityQuery;
        }
        
        /// <summary>
        /// Returns information about a city with a given cityId
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        [HttpGet("cities/{cityId}")]
        [ProducesResponseType(typeof(JsonApiDocument<CityDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCity(long cityId)
        {
            var city = await _getCityQuery.Handle(cityId);
            
            if (city == null)
            {
                return GetCityNotFoundMessage(cityId);
            }

            CityDto cityDto = _mapper.Map<CityDto>(city);
            
            return Ok(new JsonApiDocument<CityDto>(cityDto));
        }
        
        private NotFoundObjectResult GetCityNotFoundMessage(long roomId)
        {
            return NotFound($"No city with Id: {roomId} has been found");
        }
    }
}