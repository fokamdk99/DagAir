using System.Net;
using DagAir.Components.ApiModels.Json;
using DagAir.QueryNode;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.Facilities.Infrastructure
{
    [ApiController]
    [Route("query/" + ApiVersions.V1)]
    public abstract class QueryNodeControllerBase : ControllerBase
    {
        protected CreatedResult Created(object value) => Created(string.Empty, value);

        protected NotFoundObjectResult NotFound(string message) =>
            NotFound(new JsonApiErrorDocument(new JsonApiError(HttpStatusCode.NotFound, message)));
    }
}