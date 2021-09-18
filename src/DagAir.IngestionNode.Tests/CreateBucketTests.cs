using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using NUnit.Framework;

namespace DagAir.IngestionNode.Tests
{
    public class Tests
    {
        private static readonly char[] Token = "04QQQNYZs0E1KWImbwYgWBDPg6m6AhI-uATbrJzgEbMBPcPGI8g_iCrAQeD5yBitaoxKmtUvrijHQsdfIz2I1A==".ToCharArray();
        
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            string bucketName = "dagair_bucket5";
            string orgId = "a4d3426e45798e43";
            var influxDBClient = InfluxDBClientFactory.Create("http://localhost:8086", Token);
            
            // create bucket 'dagair_bucket' with data retention set to 3600 seconds
            var retention = new BucketRetentionRules(BucketRetentionRules.TypeEnum.Expire, 3600);

            var bucket = await influxDBClient.GetBucketsApi().CreateBucketAsync(bucketName, retention, orgId);
            
            // create access token to 'dagair_bucket'
            var resource = new PermissionResource
            {
                Id = bucket.Id,
                Org = "dagair",
                Name = "resourceName",
                OrgID = orgId, 
                Type = PermissionResource.TypeEnum.Buckets
            };

            Assert.That(resource != null);
            
            // read permission
            var read = new Permission(Permission.ActionEnum.Read, resource);
            var write = new Permission(Permission.ActionEnum.Write, resource);

            var authorization = await influxDBClient.GetAuthorizationsApi()
                .CreateAuthorizationAsync(orgId, new List<Permission> {read, write});

            // created token that can be used for writes to iot_bucket
            var token = authorization.Token;
            Console.WriteLine($"Token: {token}");
            
            influxDBClient.Dispose();
        }
    }
}