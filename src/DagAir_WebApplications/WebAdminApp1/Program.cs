using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DagAir.Components.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebAdminApp1
{
    public class Program
    {
        private static string[]? _urls; 
        
        public static void Main(string[] args)
        {
            ConfigureEnvironmentVariables();
            
            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseConsoleLifetime()
                .UseDagAirLogger()
                .ConfigureServices((hostBuilderContext, services) =>
                {
                    services
                        .AddWebAdminAppFeature();
                })
                .ConfigureWebHostDefaults(ConfigureWebHost);

        private static void ConfigureWebHost(IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.UseStartup<Startup>();
            webHostBuilder.UseKestrel();
            webHostBuilder.UseUrls(_urls!);
        }

        private static void ConfigureEnvironmentVariables()
        {
            var urls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ?? throw new Exception(
                "ASPNETCORE_URLS configuration is missing. Please verify that Launchsettings.json has required value.");
            _urls = urls!.Split(";");
        }
    }
}
