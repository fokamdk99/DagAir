using System;
using InfluxDB.Client.Core;

namespace DagAir.IngestionNode.Data.Measurements
{
    // Public class
    [Measurement("influxroommeasurement")]
    public class InfluxRoomMeasurement
    {
        [Column("sensor_id", IsTag = true)] public string SensorId { get; set; }
        [Column("temperature")] public double? Temperature { get; set; }
        [Column("humidity")] public double? Humidity { get; set; }
        [Column("illuminance")] public double? Illuminance { get; set; }
        [Column(IsTimestamp = true)] public DateTime Time { get; set; }
    }
}