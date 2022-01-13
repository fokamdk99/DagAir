using System;
using InfluxDB.Client.Core;

namespace DagAir.IngestionNode.Data.Measurements
{
    // Public class
    [Measurement("influxroommeasurement")]
    public class InfluxRoomMeasurement
    {
        [Column("sensor_id", IsTag = true)] public string SensorId { get; set; }
        [Column("temperature")] public decimal? Temperature { get; set; }
        [Column("humidity")] public decimal? Humidity { get; set; }
        [Column("illuminance")] public int? Illuminance { get; set; }
        [Column(IsTimestamp = true)] public DateTime Time { get; set; }
    }
}