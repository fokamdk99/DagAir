using System;
using System.IO;
using System.Threading.Tasks;
using DagAir.Components.Logging;
using DagAir.Policies.Data.AppContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace DagAir.Policies.Tests
{
    [Category("Integration")]
    public abstract class IntegrationTestServer
    {
        protected IConfiguration Configuration;
        protected IHost TestServer;

        protected IDagAirPoliciesAppContext AppContext;
        private IServiceScope _scope;

        protected string DatabaseName { get; set; }

        [SetUp]
        public async Task SetupTest()
        {
            DatabaseName = "Policies" + Guid.NewGuid();
            _scope = TestServer.Services.CreateScope();
            AppContext = _scope.ServiceProvider.GetRequiredService<IDagAirPoliciesAppContext>();

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
                    webBuilder.ConfigureServices(ConfigurePoliciesServices);
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

        private void ConfigurePoliciesServices(IServiceCollection services)
        {
            services.AddPoliciesFeature(Configuration);
            services.AddSingleton<InMemoryDagAirPoliciesAppContextProvider>();
            services.AddScoped<IDagAirPoliciesAppContext, DagAirPoliciesAppContext>(x =>
            {
                var provider = x.GetRequiredService<InMemoryDagAirPoliciesAppContextProvider>();
                return provider.Provide(DatabaseName);
            });
        }
    }
}