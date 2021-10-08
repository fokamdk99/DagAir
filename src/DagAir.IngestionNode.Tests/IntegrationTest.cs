using System.Threading.Tasks;
using DagAir.IngestionNode.Data;
using DagAir.IngestionNode.Data.Influx;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DagAir.IngestionNode.Tests
{
    [Category("Integration")]
    public abstract class IntegrationTest
    {
        protected IInfluxConfiguration InfluxConfiguration { get; private set; }
        
        [SetUp]
        public async Task Setup()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
            
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddIngestionNodeDataFeature(configuration);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            
            InfluxConfiguration = serviceProvider.GetRequiredService<IInfluxConfiguration>();
        }

        protected virtual Task SetupPreHost()
        {
            return Task.CompletedTask;
        }

        protected abstract void AddOverrides(IServiceCollection services);
        
        [TearDown]
        protected abstract Task CleanUp();
    }
}