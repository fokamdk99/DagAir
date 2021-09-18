using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;

namespace DagAir.IngestionNode.Data.Buckets
{
    
    public static class CreateBucketService
    {
        private static readonly char[] Token = "04QQQNYZs0E1KWImbwYgWBDPg6m6AhI-uATbrJzgEbMBPcPGI8g_iCrAQeD5yBitaoxKmtUvrijHQsdfIz2I1A==".ToCharArray();

        public static async Task<string> CreateBucket(string bucketName, string orgId, string url, int retention)
        {
            
            var influxDBClient = InfluxDBClientFactory.Create(url, Token);
            
            // create bucket 'dagair_bucket' with data retention set to 3600 seconds
            var retentionRules = new BucketRetentionRules(BucketRetentionRules.TypeEnum.Expire, retention);

            var bucket = await influxDBClient.GetBucketsApi().CreateBucketAsync(bucketName, retentionRules, orgId);
            
            // create access token to 'dagair_bucket'
            var resource = new PermissionResource
            {
                Id = bucket.Id,
                OrgID = orgId, 
                Type = PermissionResource.TypeEnum.Buckets
            };

            // read permission
            var read = new Permission(Permission.ActionEnum.Read, resource);
            var write = new Permission(Permission.ActionEnum.Write, resource);

            var authorization = await influxDBClient.GetAuthorizationsApi()
                .CreateAuthorizationAsync(orgId, new List<Permission> {read, write});

            // created token that can be used for writes to iot_bucket
            var token = authorization.Token;

            influxDBClient.Dispose();

            return token;
        }
        
        
    }
}