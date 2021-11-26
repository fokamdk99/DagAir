using System.Net;
using DagAir.Components.ApiModels.Json;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.Facilities.Infrastructure
{
    [ApiController]
    [Route("facilities/" + ApiVersions.V1)]
    public abstract class FacilitiesControllerBase : ControllerBase
    {
        protected CreatedResult Created(object value) => Created(string.Empty, value);

        protected NotFoundObjectResult NotFound(string message) =>
            NotFound(new JsonApiErrorDocument(new JsonApiError(HttpStatusCode.NotFound, message)));
    }
}