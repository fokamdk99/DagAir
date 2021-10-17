using System.Net;
using System.Threading.Tasks;
using DagAir.Components.ApiModels.Json;
using DagAir.Sensors.Infrastructure.UserApi;
using DagAir.Sensors.Sensor.Models;
using DagAir.Sensors.Sensor.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.Sensors.Sensor
{
    public class SensorsController : SensorControllerBase
    {
        private readonly IQuery<SensorReadModel> _getCurrentSensorsWithRelatedEntitiesQuery;

        public SensorsController(IQuery<SensorReadModel> getCurrentSensorsWithRelatedEntitiesQuery)
        {
            _getCurrentSensorsWithRelatedEntitiesQuery = getCurrentSensorsWithRelatedEntitiesQuery;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(JsonApiDocument<SensorReadModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCurrentSensorsWithRelatedEntitiesQuery(long id)
        {
            var sensor = await _getCurrentSensorsWithRelatedEntitiesQuery.Execute(id);
            if (sensor == null)
            {
                return GetCurrentSensorNotFoundMessage(id);
            }
            return Ok(new JsonApiDocument<SensorReadModel>(sensor));
        }
        
        private NotFoundObjectResult GetCurrentSensorNotFoundMessage(long id)
        {
            return NotFound($"A current room with Id: {id} has not been found");
        }
    }
}