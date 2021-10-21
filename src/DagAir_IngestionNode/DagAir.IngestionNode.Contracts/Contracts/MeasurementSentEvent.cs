namespace DagAir.IngestionNode.Contracts
{
    public class MeasurementSentEvent : IMeasurement
    {
        public float Temperature { get; set; }
        public float Illuminance { get; set; } //light intensity
        public float Humidity { get; set; }
        public long RoomId { get; set; }

        public MeasurementSentEvent(float temperature, float illuminance, float humidity, long roomId)
        {
            Temperature = temperature;
            Illuminance = illuminance;
            Humidity = humidity;
            RoomId = roomId;
        }
    }
}