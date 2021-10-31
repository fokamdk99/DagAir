using System.Collections.Generic;

namespace DagAir.Sensors.Data.AppEntities
{
    public class SensorModel : AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public long ProducerId { get; set; }
        public virtual Producer Producer { get; set; }
        public virtual ICollection<Sensor> Sensors { get; set; }
    }
}