using System;
using System.IO;
using System.Threading.Tasks;
using DagAir.Components.Logging;
using DagAir.Sensors.Data.AppContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace DagAir.Sensors.Tests
{
    [Category("Integration")]
    public abstract class IntegrationTestServer
    {
        protected IConfiguration Configuration;
        protected IHost TestServer;

        protected IDagAirSensorAppContext AppContext;
        private IServiceScope _scope;

        protected string DatabaseName { get; set; }

        [SetUp]
        public async Task SetupTest()
        {
            DatabaseName = "Sensors" + Guid.NewGuid();
            _scope = TestServer.Services.CreateScope();
            AppContext = _scope.ServiceProvider.GetRequiredService<IDagAirSensorAppContext>();

            await Setup();
        }

        public virtual async Task Setup()
        {
            await Task.CompletedTask;
        }

        [OneTimeSetUp]
        public virtual async Task OneTimeSetup()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Integration.json")
                .AddEnvironmentVariables()
                .Build();

            var hostBuilder = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(builder => builder.AddConfiguration(Configuration))
                .UseDagAirLogger()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseTestServer();
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureServices(ConfigureServices);
                });

            TestServer = await hostBuilder.StartAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            await Cleanup();
            _scope?.Dispose();
        }

        public virtual Task Cleanup()
        {
            return Task.CompletedTask;
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSensorsFeature();
            services.AddSingleton<InMemoryDagAirSensorAppContextProvider>();
            services.AddScoped<IDagAirSensorAppContext, DagAirSensorAppContext>(x =>
            {
                var provider = x.GetRequiredService<InMemoryDagAirSensorAppContextProvider>();
                return provider.Provide(DatabaseName);
            });
        }
    }
}