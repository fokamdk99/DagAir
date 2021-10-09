using System;
using System.Threading.Tasks;
using DagAir.IngestionNode.Data;
using DagAir.IngestionNode.Data.Influx;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DagAir.IngestionNode.Tests
{
    [Category("Integration")]
    public abstract class IntegrationTest
    {
        protected InfluxDBClient Client;
        protected Bucket TestBucket;
        protected IInfluxConfiguration InfluxConfiguration { get; private set; }
        
        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
            
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddIngestionNodeDataFeature(configuration);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            
            InfluxConfiguration = serviceProvider.GetRequiredService<IInfluxConfiguration>();
            
            //Client = InfluxDBClientFactory.Create(InfluxConfiguration.Url, InfluxConfiguration.Token);
            Client = serviceProvider.GetRequiredService<InfluxDBClient>();
            var retention = new BucketRetentionRules(BucketRetentionRules.TypeEnum.Expire, InfluxConfiguration.Retention);
            TestBucket = await Client.GetBucketsApi().CreateBucketAsync(InfluxConfiguration.BucketName, retention, InfluxConfiguration.OrgId);
        }

        [SetUp]
        protected virtual async Task Setup()
        {
            await Client.GetDeleteApi().Delete(DateTime.Now.AddSeconds(InfluxConfiguration.Retention), DateTime.Now, "_measurement=\"influxroommeasurement\"",
                InfluxConfiguration.BucketName, InfluxConfiguration.Org);
        }

        protected virtual Task SetupPreHost()
        {
            return Task.CompletedTask;
        }

        protected abstract void AddOverrides(IServiceCollection services);

        [OneTimeTearDown]
        protected virtual async Task CleanUp()
        {
            await Client.GetBucketsApi().DeleteBucketAsync(TestBucket);
            Client.Dispose();
        }
    }
}