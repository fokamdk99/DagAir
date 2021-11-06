using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DagAir.Policies.Tests
{
    public static class PoliciesApiTestClient
    {
        public static HttpClient GetTestClient(IHost testServer, IConfiguration configuration)
        {
            var client = testServer.GetTestClient();

            return client;
        }
    }
}