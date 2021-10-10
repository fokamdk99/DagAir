using DagAir.IngestionNode.Contracts;

namespace DagAir.PolicyNode.Data.AppEntities
{
    public class ExpectedRoomConditions : AuditableEntity, IMeasurement
    {
        public long Id { get; set; }
        public float Temperature { get; set; }
        public float Illuminance { get; set; } //light intensity
        public float Humidity { get; set; }
        public virtual RoomPolicy RoomPolicy { get; set; }
    }
}