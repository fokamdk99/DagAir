using System.Net;
using DagAir.Components.ApiModels.Json;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.Sensors.Infrastructure.UserApi
{
    [ApiController]
    [Route("V1/sensors")]
    public class SensorControllerBase : ControllerBase
    {
        protected CreatedResult Created(object value) => Created(string.Empty, value);

        protected NotFoundObjectResult NotFound(string message) =>
            NotFound(new JsonApiErrorDocument(new JsonApiError(HttpStatusCode.NotFound, message)));
    }
}