using System.Net;
using System.Threading.Tasks;
using DagAir.DataServices.SensorState.CurrentMeasurements.Handlers;
using DagAir.DataServices.SensorState.Infrastructure.UserApi;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.DataServices.SensorState.CurrentMeasurements
{
    public class CurrentMeasurementsController : SensorStateControllerBase
    {
        private readonly IGetCurrentMeasurementHandler _getCurrentMeasurementHandler;

        public CurrentMeasurementsController(IGetCurrentMeasurementHandler getCurrentMeasurementHandler)
        {
            _getCurrentMeasurementHandler = getCurrentMeasurementHandler;
        }

        [HttpGet("current-measurements/{sensorName}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetCurrentRoomPolicy(string sensorName)
        {
            await _getCurrentMeasurementHandler.Handle(sensorName);

            return NoContent();
        }
    }
}