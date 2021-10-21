#nullable enable
using System.Net;

namespace DagAir.Components.ApiModels.Json
{
    public static class JsonApiErrorDocumentWrapper
    {
        public static JsonApiErrorDocument ToJsonApiErrorDocument(HttpStatusCode code, string? message)
        {
            return new(new JsonApiError(code, message));
        }
    }
}