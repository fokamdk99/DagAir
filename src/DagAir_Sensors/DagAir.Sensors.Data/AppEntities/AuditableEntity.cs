using System;

namespace DagAir.Sensors.Data.AppEntities
{
    public abstract class AuditableEntity
    {
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}