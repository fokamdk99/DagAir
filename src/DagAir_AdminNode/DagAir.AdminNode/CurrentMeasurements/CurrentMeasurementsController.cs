using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DagAir.AdminNode.Contracts.DTOs;
using DagAir.AdminNode.Infrastructure.UserApi;
using DagAir.AdminNode.Sensors;
using DagAir.Components.ApiModels.Json;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.AdminNode.CurrentMeasurements
{
    public class CurrentMeasurementsController : AdminControllerBase
    {
        private readonly ISensorsHandler _sensorsHandler;
        private readonly ICurrentMeasurementsHandler _currentMeasurementsHandler;

        public CurrentMeasurementsController(ISensorsHandler sensorsHandler, ICurrentMeasurementsHandler currentMeasurementsHandler)
        {
            _sensorsHandler = sensorsHandler;
            _currentMeasurementsHandler = currentMeasurementsHandler;
        }

        /*
        [HttpGet]
        [Route("current-measurements")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetCurrentMeasurement(int roomId)
        {
            var sensorDto = await _sensorsHandler.GetSensorByRoomId(roomId);

            await _currentMeasurementsHandler.Handle(sensorDto.SensorName);

            return NoContent();
        }
        */
    }
}