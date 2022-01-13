namespace DagAir.IngestionNode.Contracts
{
    public class RoomMeasurement : IMeasurement
    {
        public decimal Temperature { get; set; }
        public int Illuminance { get; set; } //light intensity
        public decimal Humidity { get; set; }

        public RoomMeasurement(decimal temperature, int illuminance, decimal humidity)
        {
            Temperature = temperature;
            Illuminance = illuminance;
            Humidity = humidity;
        }
    }
}