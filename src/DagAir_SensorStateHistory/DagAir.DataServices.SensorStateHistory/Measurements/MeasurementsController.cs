using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DagAir.Components.ApiModels.Json;
using DagAir.DataServices.SensorStateHistory.Contracts.Commands;
using DagAir.DataServices.SensorStateHistory.Contracts.Contracts;
using DagAir.DataServices.SensorStateHistory.Infrastructure.UserApi;
using DagAir.DataServices.SensorStateHistory.Measurements.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.DataServices.SensorStateHistory.Measurements
{
    public class MeasurementsController : SensorStateHistoryControllerBase
    {
        private readonly IGetMeasurementsFromSensorQuery _getMeasurementsFromSensorQuery;

        public MeasurementsController(IGetMeasurementsFromSensorQuery getMeasurementsFromSensorQuery)
        {
            _getMeasurementsFromSensorQuery = getMeasurementsFromSensorQuery;
        }

        [HttpPost("historic-measurements")]
        [ProducesResponseType(typeof(JsonApiDocument<List<HistoricMeasurement>>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetMeasurementsFromSensor([FromBody]
            GetMeasurementsFromSensorCommand getMeasurementsFromSensorCommand)
        {
            var historicMeasurements =
                await _getMeasurementsFromSensorQuery.GetMeasurementsFromSensor(getMeasurementsFromSensorCommand);

            return Ok(new JsonApiDocument<List<HistoricMeasurement>>(historicMeasurements));
        }
    }
}