using System;

namespace DagAir.Sensors.Data.AppEntities
{
    public class Sensor : AuditableEntity
    {
        public long Id { get; set; }
        public string SensorName { get; set; }
        public DateTime LastDataSentDate { get; set; }
        public long SensorModelId { get; set; }
        public long RoomId { get; set; }
        public long AffiliateId { get; set; }
        public virtual SensorModel SensorModel { get; set; }
    }
}