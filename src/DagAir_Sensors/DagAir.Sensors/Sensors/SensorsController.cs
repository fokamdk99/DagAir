﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DagAir.Components.ApiModels.Json;
using DagAir.Sensors.Contracts.Commands;
using DagAir.Sensors.Contracts.DTOs;
using DagAir.Sensors.Infrastructure.UserApi;
using DagAir.Sensors.Sensors.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.Sensors.Sensors
{
    public class SensorsController : SensorControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGetSensorWithRelatedEntitiesQuery _getSensorWithRelatedEntitiesQuery;
        private readonly IGetAllSensorsWithRelatedEntitiesQuery _getAllSensorsWithRelatedEntitiesQuery;
        private readonly IGetSensorBySensorName _getSensorBySensorName;
        private readonly IGetSensorByRoomId _getSensorByRoomId;

        public SensorsController(IMapper mapper, 
            IGetSensorWithRelatedEntitiesQuery getCurrentSensorsWithRelatedEntitiesQuery,
            IGetAllSensorsWithRelatedEntitiesQuery getAllSensorsWithRelatedEntitiesQuery,
            IGetSensorBySensorName getSensorBySensorName, 
            IGetSensorByRoomId getSensorByRoomId)
        {
            _mapper = mapper;
            _getSensorWithRelatedEntitiesQuery = getCurrentSensorsWithRelatedEntitiesQuery;
            _getAllSensorsWithRelatedEntitiesQuery = getAllSensorsWithRelatedEntitiesQuery;
            _getSensorBySensorName = getSensorBySensorName;
            _getSensorByRoomId = getSensorByRoomId;
        }
        
        /// <summary>
        /// Returns information about all sensors
        /// </summary>
        /// <returns></returns>
        [HttpGet("sensors")]
        [ProducesResponseType(typeof(JsonApiDocument<List<SensorDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllSensorsWithRelatedEntitiesQuery()
        {
            var sensors = await _getAllSensorsWithRelatedEntitiesQuery.Execute();

            List<SensorDto> sensorDtos = new List<SensorDto>();
            sensorDtos.AddRange(sensors.Select(s => _mapper.Map<SensorDto>(s)));
            
            return Ok(new JsonApiDocument<List<SensorDto>>(sensorDtos));
        }
        
        /// <summary>
        /// Returns information about a sensor with a given sensorId
        /// </summary>
        /// <param name="sensorId"></param>
        /// <returns></returns>
        [HttpGet("sensors/{sensorId}")]
        [ProducesResponseType(typeof(JsonApiDocument<SensorDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSensorWithRelatedEntitiesQuery(long sensorId)
        {
            var sensor = await _getSensorWithRelatedEntitiesQuery.Execute(sensorId);
            if (sensor == null)
            {
                return GetCurrentSensorNotFoundMessage(sensorId);
            }

            var sensorDto = _mapper.Map<SensorDto>(sensor);
            return Ok(new JsonApiDocument<SensorDto>(sensorDto));
        }
        
        [HttpGet("sensors/room-id/{roomId}")]
        [ProducesResponseType(typeof(JsonApiDocument<SensorDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSensorByRoomId(long roomId)
        {
            var sensor = await _getSensorByRoomId.Execute(roomId);
            if (sensor == null)
            {
                return GetCurrentSensorByRoomIdNotFoundMessage(roomId);
            }

            var sensorDto = _mapper.Map<SensorDto>(sensor);
            return Ok(new JsonApiDocument<SensorDto>(sensorDto));
        }

        [HttpPost("sensors/sensor-name")]
        [ProducesResponseType(typeof(JsonApiDocument<SensorDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSensorBySensorName([FromBody] GetSensorBySensorNameCommand getSensorBySensorNameCommand)
        {
            var sensor = await _getSensorBySensorName.Execute(getSensorBySensorNameCommand.SensorName);
            if (sensor == null)
            {
                return GetCurrentSensorBySensorNameNotFoundMessage(getSensorBySensorNameCommand.SensorName);
            }

            var sensorDto = _mapper.Map<SensorDto>(sensor);
            return Ok(new JsonApiDocument<SensorDto>(sensorDto));
        }
        
        private NotFoundObjectResult GetCurrentSensorNotFoundMessage(long id)
        {
            return NotFound($"No sensor with Id: {id} has been found");
        }

        private NotFoundObjectResult GetCurrentSensorBySensorNameNotFoundMessage(string sensorName)
        {
            return NotFound($"No sensor with name: {sensorName} has been found");
        }
        
        private NotFoundObjectResult GetCurrentSensorByRoomIdNotFoundMessage(long roomId)
        {
            return NotFound($"No sensor with roomId: {roomId} has been found");
        }
    }
}