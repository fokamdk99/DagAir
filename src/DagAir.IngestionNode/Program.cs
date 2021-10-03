using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DagAir.IngestionNode.Infrastructure;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DagAir.IngestionNode
{
    class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .Build();
            
            CreateHostBuilder(args, configuration)
                .Build()
                .Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webHostBuilder =>
                {
                    ConfigureWebHost(webHostBuilder, GetUrls(configuration));
                });

        private static void ConfigureWebHost(IWebHostBuilder webHostBuilder, string[] urls)
        {
            webHostBuilder.UseStartup<Startup>();
            webHostBuilder.UseKestrel();
            webHostBuilder.UseUrls(urls);
        }

        private static string[] GetUrls(IConfiguration configuration)
        {
            return configuration
                .GetSection("webHostingUrls")
                .GetChildren()
                .Select(c => c.Value)
                .ToArray();
        }
    }
}
