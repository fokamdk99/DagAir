using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DagAir.IngestionNode.Tests.Data.Migrations.Influx
{
    public class Tests : IntegrationTest
    {
        private InfluxDBClient _client;
        private string CreatedBucketId { get; set; }

        [Test]
        public async Task WhenCreateBucketApiUsed_ShouldCreateNewBucket()
         {
             _client = InfluxDBClientFactory.Create(InfluxConfiguration.Url, InfluxConfiguration.Token);
             var retention = new BucketRetentionRules(BucketRetentionRules.TypeEnum.Expire, InfluxConfiguration.Retention);
             var bucket = await _client.GetBucketsApi().CreateBucketAsync(InfluxConfiguration.BucketName, retention, InfluxConfiguration.OrgId);
             CreatedBucketId = bucket.Id;
             Assert.That(bucket != null);
         }

        protected override void AddOverrides(IServiceCollection services)
        {
            
        }

        [TearDown]
        protected override async Task CleanUp()
        {
            await _client.GetBucketsApi().DeleteBucketAsync(CreatedBucketId);
            _client.Dispose();
        }
    }
}