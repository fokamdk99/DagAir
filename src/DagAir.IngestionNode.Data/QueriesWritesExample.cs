using System;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core;
using InfluxDB.Client.Writes;
using Task = System.Threading.Tasks.Task;

namespace DagAir.IngestionNode.Data
{
    public static class QueriesWritesExample
    {
        private static readonly char[] Token = "".ToCharArray();

        public static async Task MainWritesQueries(string[] args)
        {
            var influxDBClient = InfluxDBClientFactory.Create("http://localhost:8086", Token);
            
            // write data
            using (var writeApi = influxDBClient.GetWriteApi())
            {
                var point = PointData.Measurement("temperature")
                    .Tag("location", "Warsaw")
                    .Field("value", 17)
                    .Timestamp(DateTime.UtcNow.AddSeconds(-10), WritePrecision.Ms);
                
                writeApi.WritePoint("bucket_name", "org_id", point);
                
                // query data
                var flux = "from(bucket:\"temperature-sensors\") |> range(start: 0)";

                var fluxTables = await influxDBClient.GetQueryApi().QueryAsync(flux, "org_id");
                fluxTables.ForEach(fluxTable =>
                {
                    var fluxRecords = fluxTable.Records;
                    fluxRecords.ForEach(fluxRecord =>
                    {
                        Console.WriteLine($"{fluxRecord.GetTime()}: {fluxRecord.GetValue()}");
                    });
                });
                
                influxDBClient.Dispose();
            }
        }
    }
}