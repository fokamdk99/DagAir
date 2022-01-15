using System.Net;
using System.Threading.Tasks;
using DagAir.AdminNode.Contracts.Commands;
using DagAir.AdminNode.Contracts.DTOs;
using DagAir.AdminNode.Infrastructure.UserApi;
using DagAir.Components.ApiModels.Json;
using DagAir.Facilities.Contracts.DTOs;
using DagAir.Policies.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.AdminNode.SensorStateHistory
{
    public class SensorStateHistoryAdminController : AdminControllerBase
    {
        private readonly ISensorStateHistoryHandler _sensorStateHistoryHandler;

        public SensorStateHistoryAdminController(ISensorStateHistoryHandler sensorStateHistoryHandler)
        {
            _sensorStateHistoryHandler = sensorStateHistoryHandler;
        }

        [HttpPost]
        [Route("rooms/historic-measurements")]
        [ProducesResponseType(typeof(JsonApiDocument<PastMeasurementsDto>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetHistoricMeasurements([FromBody] GetRoomCommand getRoomCommand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var adminNodeRoomDto = await _sensorStateHistoryHandler.GetHistoricMeasurements(getRoomCommand);
            
            return Ok(new JsonApiDocument<PastMeasurementsDto>(adminNodeRoomDto));
        }
    }
}