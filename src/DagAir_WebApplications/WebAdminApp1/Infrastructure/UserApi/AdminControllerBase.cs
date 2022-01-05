using System.Net;
using DagAir.Components.ApiModels.Json;
using Microsoft.AspNetCore.Mvc;

namespace WebAdminApp1.Infrastructure.UserApi
{
    [ApiController]
    [Route("admin")]
    [ApiVersion(ApiVersions.V1)]
    public class AdminControllerBase : ControllerBase
    {
        protected CreatedResult Created(object value) => Created(string.Empty, value);

        protected NotFoundObjectResult NotFound(string message) =>
            NotFound(new JsonApiErrorDocument(new JsonApiError(HttpStatusCode.NotFound, message)));
    }
}