using System.Net;
using DagAir.Components.ApiModels.Json;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.DataServices.SensorState.Infrastructure.UserApi
{
    [ApiController]
    [Route("sensorstate-api")]
    [ApiVersion(SensorStateApiVersions.V1)]
    public class SensorStateControllerBase : ControllerBase
    {
        protected CreatedResult Created(object value) => Created(string.Empty, value);

        protected NotFoundObjectResult NotFound(string message) =>
            NotFound(new JsonApiErrorDocument(new JsonApiError(HttpStatusCode.NotFound, message)));
    }
}