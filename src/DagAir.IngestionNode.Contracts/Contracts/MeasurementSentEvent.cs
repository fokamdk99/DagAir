namespace DagAir.IngestionNode.Contracts
{
    public class MeasurementSentEvent : IMeasurement
    {
        public float Temperature { get; set; }
        public float Illuminance { get; set; } //light intensity
        public float Humidity { get; set; }
        public int RoomId { get; set; }

        public MeasurementSentEvent(float temperature, float illuminance, float humidity, int roomId)
        {
            Temperature = temperature;
            Illuminance = illuminance;
            Humidity = humidity;
            RoomId = roomId;
        }
    }
}