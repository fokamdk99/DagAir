using InfluxDB.Client.Core;

namespace DagAir.IngestionNode.Data.Measurements
{
    [Measurement("influxroommeasurement")]
    public class InfluxRoomMeasurement
    {
        [Column("sensor_name", IsTag = true)] public string SensorName { get; set; }
        [Column("temperature")] public decimal? Temperature { get; set; }
        [Column("humidity")] public decimal? Humidity { get; set; }
        [Column("illuminance")] public int? Illuminance { get; set; }
    }
}