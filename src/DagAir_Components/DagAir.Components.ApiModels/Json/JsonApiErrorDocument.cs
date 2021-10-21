using System.Collections.Generic;
using System.Linq;

namespace DagAir.Components.ApiModels.Json
{
    public class JsonApiErrorDocument
    {
        public List<JsonApiError> Errors { get; set; }

        public JsonApiErrorDocument(JsonApiError error)
        {
            Errors = new List<JsonApiError>();
            Errors.Add(error);
        }
        
        public JsonApiErrorDocument(IEnumerable<JsonApiError> errors)
        {
            Errors = errors.ToList();
        }
    }
}