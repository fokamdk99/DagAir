using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DagAir.Components.HttpClients
{
    public interface IDagAirHttpClient
    {
        Task<(T, HttpStatusCode)> GetAsync<T>(string url);
        Task<HttpResponseMessage> PostAsync<T>(string url, T request);
        Task<HttpStatusCode> PutAsync<T>(string url, T request);
        Task<HttpStatusCode> DeleteAsync(string url);
    }
}