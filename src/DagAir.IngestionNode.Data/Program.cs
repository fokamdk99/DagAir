using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.IngestionNode.Data.Buckets;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;


namespace DagAir.IngestionNode.Data
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            string bucketName = "dagair_bucket";
            string orgId = "a4d3426e45798e43";
            string url = "http://localhost:8086";
            int retention = 3600;
            string bucketToken = await CreateBucketService.CreateBucket(bucketName, orgId, url, retention);
            Console.WriteLine($"token: {bucketToken}");
        }
    }
}