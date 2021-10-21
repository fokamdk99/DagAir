namespace DagAir.IngestionNode.Contracts
{
    public class RoomMeasurement : IMeasurement
    {
        public float Temperature { get; set; }
        public float Illuminance { get; set; } //light intensity
        public float Humidity { get; set; }

        public RoomMeasurement(float temperature, float illuminance, float humidity)
        {
            Temperature = temperature;
            Illuminance = illuminance;
            Humidity = humidity;
        }
    }
}