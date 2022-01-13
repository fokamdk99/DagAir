namespace DagAir.IngestionNode.Contracts
{
    public class MeasurementSentEvent : IMeasurement
    {
        public decimal Temperature { get; set; }
        public int Illuminance { get; set; } //light intensity
        public decimal Humidity { get; set; }
        public long RoomId { get; set; }

        public MeasurementSentEvent(decimal temperature, int illuminance, decimal humidity, long roomId)
        {
            Temperature = temperature;
            Illuminance = illuminance;
            Humidity = humidity;
            RoomId = roomId;
        }
    }
}