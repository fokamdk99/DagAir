using System.Collections.Generic;
using DagAir.IngestionNode.Contracts;

namespace DagAir.Policies.Data.AppEntities
{
    public class ExpectedRoomConditions : AuditableEntity, IMeasurement
    {
        public long Id { get; set; }
        public decimal Temperature { get; set; }
        public int Illuminance { get; set; } //light intensity
        public decimal Humidity { get; set; }
        public decimal TemperatureMargin { get; set; }
        public decimal IlluminanceMargin { get; set; }
        public decimal HumidityMargin { get; set; }
        public long? RoomPolicyId { get; set; }
        public virtual ICollection<RoomPolicy> RoomPolicies { get; set; }
    }
}