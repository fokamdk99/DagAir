using System;

namespace DagAir.Facilities.Data.AppEntitities
{
    public abstract class AuditableEntity
    {
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}