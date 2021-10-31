using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DagAir.Components.ApiModels.Json;
using DagAir.Sensors.Contracts.DTOs;
using DagAir.Sensors.Infrastructure.UserApi;
using DagAir.Sensors.Sensors.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.Sensors.Sensors
{
    [Route("sensors")]
    public class SensorsController : SensorControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGetSensorWithRelatedEntitiesQuery _getSensorWithRelatedEntitiesQuery;
        private readonly IGetAllSensorsWithRelatedEntitiesQuery _getAllSensorsWithRelatedEntitiesQuery;

        public SensorsController(IMapper mapper, IGetSensorWithRelatedEntitiesQuery getCurrentSensorsWithRelatedEntitiesQuery,
            IGetAllSensorsWithRelatedEntitiesQuery getAllSensorsWithRelatedEntitiesQuery)
        {
            _mapper = mapper;
            _getSensorWithRelatedEntitiesQuery = getCurrentSensorsWithRelatedEntitiesQuery;
            _getAllSensorsWithRelatedEntitiesQuery = getAllSensorsWithRelatedEntitiesQuery;
        }
        
        [HttpGet("sensors")]
        [ProducesResponseType(typeof(JsonApiDocument<List<SensorDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllSensorsWithRelatedEntitiesQuery()
        {
            var sensors = await _getAllSensorsWithRelatedEntitiesQuery.Execute();

            List<SensorDto> sensorDtos = new List<SensorDto>();
            sensorDtos.AddRange(sensors.Select(s => _mapper.Map<SensorDto>(s)));
            
            return Ok(new JsonApiDocument<List<SensorDto>>(sensorDtos));
        }
        
        [HttpGet("sensors/{id}")]
        [ProducesResponseType(typeof(JsonApiDocument<SensorDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSensorWithRelatedEntitiesQuery(long id)
        {
            var sensor = await _getSensorWithRelatedEntitiesQuery.Execute(id);
            if (sensor == null)
            {
                return GetCurrentSensorNotFoundMessage(id);
            }

            var sensorDto = _mapper.Map<SensorDto>(sensor);
            return Ok(new JsonApiDocument<SensorDto>(sensorDto));
        }
        
        private NotFoundObjectResult GetCurrentSensorNotFoundMessage(long id)
        {
            return NotFound($"A current room with Id: {id} has not been found");
        }
    }
}