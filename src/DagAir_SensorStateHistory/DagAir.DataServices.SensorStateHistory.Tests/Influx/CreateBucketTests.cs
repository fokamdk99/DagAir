using System.Threading.Tasks;
using DagAir.Components.Influx;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace DagAir.DataServices.SensorStateHistory.Tests.Influx
{
    public class Tests
    {
        private string CreatedBucketId { get; set; }
        private InfluxDBClient Client { get; set; }
        private IInfluxConfiguration InfluxConfiguration { get; set; }
        private IInfluxHelper InfluxHelper { get; set; }
        private IHost TestHost { get; set; } 

        [SetUp]
        public async Task Setup()
        {
            TestHost = HostProvider.Create();
            Client = TestHost.Services.GetRequiredService<InfluxDBClient>();
            InfluxConfiguration = TestHost.Services.GetRequiredService<IInfluxConfiguration>();
            InfluxHelper = TestHost.Services.GetRequiredService<IInfluxHelper>();
        }
        [Ignore("Influx dependent")]
        [Test]
        public async Task WhenCreateBucketApiUsed_ShouldCreateNewBucket()
         {
             var retention = new BucketRetentionRules(BucketRetentionRules.TypeEnum.Expire, InfluxConfiguration.Retention);
             var organizationId = await InfluxHelper.GetOrganizationIdByOrganizationName(Client, InfluxConfiguration);
             var bucket = await Client.GetBucketsApi().CreateBucketAsync(InfluxConfiguration.BucketName + "CreateBucketTest", retention, organizationId);
             CreatedBucketId = bucket.Id;
             Assert.That(bucket != null);
         }

        [TearDown]
        protected async Task CleanUp()
        {
            await Client.GetBucketsApi().DeleteBucketAsync(CreatedBucketId);
            Client.Dispose();
            TestHost.Dispose();
        }
    }
}