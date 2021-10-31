namespace DagAir.Policies.Contracts.DTOs
{
    public class ExpectedRoomConditionsDto
    {
        public long Id { get; set; }
        public float Temperature { get; set; }
        public float Illuminance { get; set; } //light intensity
        public float Humidity { get; set; }
        public float TemperatureMargin { get; set; }
        public float IlluminanceMargin { get; set; }
        public float HumidityMargin { get; set; }
        public long? RoomPolicyId { get; set; }
        public RoomPolicyDto RoomPolicy { get; set; }
    }
}