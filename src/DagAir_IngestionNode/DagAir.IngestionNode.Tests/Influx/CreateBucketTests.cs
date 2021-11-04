using System.Threading.Tasks;
using InfluxDB.Client.Api.Domain;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DagAir.IngestionNode.Tests.Influx
{
    public class Tests : InfluxIntegrationTest
    {
        private string CreatedBucketId { get; set; }

        [Test]
        public async Task WhenCreateBucketApiUsed_ShouldCreateNewBucket()
         {
             var retention = new BucketRetentionRules(BucketRetentionRules.TypeEnum.Expire, InfluxConfiguration.Retention);
             var bucket = await Client.GetBucketsApi().CreateBucketAsync(InfluxConfiguration.BucketName + "CreateBucketTest", retention, InfluxConfiguration.OrgId);
             CreatedBucketId = bucket.Id;
             Assert.That(bucket != null);
         }

        protected override void AddOverrides(IServiceCollection services)
        {
            
        }

        [TearDown]
        protected override async Task CleanUp()
        {
            await Client.GetBucketsApi().DeleteBucketAsync(CreatedBucketId);
            await base.CleanUp();
        }
    }
}