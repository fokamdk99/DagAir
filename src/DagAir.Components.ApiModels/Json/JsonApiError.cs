using System.Net;
using Microsoft.AspNetCore.WebUtilities;

namespace DagAir.Components.ApiModels.Json
{
    public class JsonApiError
    {
        public JsonApiError(HttpStatusCode statusCode, string? messageDetails = null)
        {
            Message = ReasonPhrases.GetReasonPhrase((int) statusCode);
            MessageDetails = messageDetails;
        }
        
        public string Message { get; }
        public string? MessageDetails { get; }
    }
}