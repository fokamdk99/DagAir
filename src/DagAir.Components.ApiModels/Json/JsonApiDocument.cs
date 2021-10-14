using System.ComponentModel.DataAnnotations;

namespace DagAir.Components.ApiModels.Json
{
    public class JsonApiDocument<T>
    {
        [Required]
        public T Data { get; set; }

        public JsonApiDocument(T data)
        {
            Data = data;
        }
    }
}