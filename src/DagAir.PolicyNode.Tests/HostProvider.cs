using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DagAir.PolicyNode.Tests
{
    public static class HostProvider
    {
        public static IHost Create(Action<IServiceCollection>? addOverrides = null)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
                    services.AddPolicyNodeFeature(configuration);

                    if (addOverrides != null)
                    {
                        addOverrides(services);
                    }
                }).Build();

            return host;
        }
    }
}