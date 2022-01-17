using System.Net;
using DagAir.Components.ApiModels.Json;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.DataServices.SensorStateHistory.Infrastructure.UserApi
{
    [ApiController]
    [Route("sensor-state-history")]
    [ApiVersion(SensorStateHistoryApiVersions.V1)]
    public class SensorStateHistoryControllerBase : ControllerBase
    {
        protected CreatedResult Created(object value) => Created(string.Empty, value);

        protected NotFoundObjectResult NotFound(string message) =>
            NotFound(new JsonApiErrorDocument(new JsonApiError(HttpStatusCode.NotFound, message)));
    }
}