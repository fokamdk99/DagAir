using System.Collections.Generic;
using System.Threading.Tasks;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;

namespace DagAir.DataServices.SensorStateHistory.Data.Migrations.Buckets
{
    
    public static class CreateBucketService
    {
        public static async Task CreateBucket(char[] token, string bucketName, string orgId, string url, int retention)
        {
            
            var influxDbClient = InfluxDBClientFactory.Create(url, token);
            
            var existingBucket = await influxDbClient.GetBucketsApi().FindBucketByNameAsync(bucketName);
            if (existingBucket != null)
            {
                return;
            }
            
            var retentionRules = new BucketRetentionRules(BucketRetentionRules.TypeEnum.Expire, retention);

            var bucket = await influxDbClient.GetBucketsApi().CreateBucketAsync(bucketName, retentionRules, orgId);
            
            var resource = new PermissionResource
            {
                Id = bucket.Id,
                OrgID = orgId, 
                Type = PermissionResource.TypeEnum.Buckets
            };
            
            var read = new Permission(Permission.ActionEnum.Read, resource);
            var write = new Permission(Permission.ActionEnum.Write, resource);

            var authorization = await influxDbClient.GetAuthorizationsApi()
                .CreateAuthorizationAsync(orgId, new List<Permission> {read, write});

            influxDbClient.Dispose();
        }
    }
}