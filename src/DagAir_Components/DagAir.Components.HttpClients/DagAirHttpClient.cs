using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DagAir.Components.ApiModels.Json;
using Microsoft.Extensions.Logging;

namespace DagAir.Components.HttpClients
{
    public class DagAirHttpClient : IDagAirHttpClient
    {
        private readonly ILogger<DagAirHttpClient> _logger;
        private readonly HttpClient _client;

        public DagAirHttpClient(HttpClient client,
            ILogger<DagAirHttpClient> logger)
        {
            _client = client;
            _logger = logger;
        }
        
        public async Task<(T, HttpStatusCode)> GetAsync<T>(string url)
        {
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStreamAsync();
            return (await DeserializeModel<T>(content), response.StatusCode);
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T request)
        {
            var serializedRequest = SerializeRequest(request);
            var response = await _client.PostAsync(url, serializedRequest);
            return response;
        }

        public async Task<HttpStatusCode> PutAsync<T>(string url, T request)
        {
            var serializedRequest = SerializeRequest(request);
            var response = await _client.PutAsync(url, serializedRequest);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteAsync(string url)
        {
            var response = await _client.DeleteAsync(url);
            return response.StatusCode;
        }
        
        private StringContent SerializeRequest<T>(T request)
        {
            return new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        }
        
        public async Task<T> DeserializeModel<T>(Stream contentStream)
        {
            try
            {
                var responseData = await JsonSerializer.DeserializeAsync<JsonApiDocument<T>>(contentStream,new JsonSerializerOptions { IgnoreNullValues = true, PropertyNameCaseInsensitive = true });
                return responseData!.Data;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while deserializing httpResponse. Exception message: {e.Message}");
                throw new Exception();
            }
        }
    }
}