using System;
using System.Threading.Tasks;
using DagAir.Components.Influx;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace DagAir.IngestionNode.Tests.Influx
{
    [Category("Integration")]
    public abstract class InfluxIntegrationTest
    {
        protected InfluxDBClient Client;
        protected Bucket TestBucket;
        protected IServiceProvider Services => CurrentHost!.Services;
        protected IInfluxConfiguration InfluxConfiguration { get; private set; }
        protected IInfluxHelper InfluxHelper => CurrentHost!.Services.GetRequiredService<IInfluxHelper>();
        protected IHost CurrentHost { get; private set; }

        [SetUp]
        public async Task Setup()
        {
            await SetupPreHost();

            CurrentHost = HostProvider.Create(AddOverrides);

            (Client, TestBucket, InfluxConfiguration) = await InfluxBucket.CreateBucketOnce(Services, Client, TestBucket, InfluxConfiguration, InfluxHelper);
            await InfluxBucket.Reset(Client, InfluxConfiguration);
            await SetupTest();
        }

        [TearDown]
        protected virtual async Task CleanUp()
        {
            Client.Dispose();
            CurrentHost?.Dispose();
        }

        protected virtual Task SetupPreHost()
        {
            return Task.CompletedTask;
        }

        protected abstract void AddOverrides(IServiceCollection services);

        protected virtual Task SetupTest()
        {
            return Task.CompletedTask;
        }
    }
}