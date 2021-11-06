using System;
using System.Threading;
using System.Threading.Tasks;
using DagAir.IngestionNode.Data.Influx;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.IngestionNode.Tests.Influx
{
    public static class InfluxBucket
    {
        private static int _createOnce;

        public static async Task<(InfluxDBClient, Bucket, IInfluxConfiguration)> CreateBucketOnce(IServiceProvider serviceProvider, InfluxDBClient client, Bucket testBucket, IInfluxConfiguration influxConfiguration)
        {
            if (Interlocked.Increment(ref _createOnce) > 1)
            {
                return (client, testBucket, influxConfiguration);
            }
            
            influxConfiguration = serviceProvider.GetRequiredService<IInfluxConfiguration>();
            
            client = serviceProvider.GetRequiredService<InfluxDBClient>();
            var retention = new BucketRetentionRules(BucketRetentionRules.TypeEnum.Expire, influxConfiguration.Retention);
            var existingBucket = await client.GetBucketsApi().FindBucketByNameAsync(influxConfiguration.BucketName);
            if (existingBucket != null)
            {
                await client.GetBucketsApi().DeleteBucketAsync(existingBucket.Id);
            }

            var organizationId = await InfluxHelper.GetOrganizationIdByOrganizationName(client, influxConfiguration);
            testBucket = await client.GetBucketsApi().CreateBucketAsync(influxConfiguration.BucketName, retention, organizationId);
            return (client, testBucket, influxConfiguration);
        }

        public static async Task Reset(InfluxDBClient client, IInfluxConfiguration influxConfiguration)
        {
            await client.GetDeleteApi().Delete(DateTime.Now.AddSeconds(influxConfiguration.Retention), DateTime.Now, "_measurement=\"influxroommeasurement\"",
                influxConfiguration.BucketName, influxConfiguration.Org);
        }
    }
}