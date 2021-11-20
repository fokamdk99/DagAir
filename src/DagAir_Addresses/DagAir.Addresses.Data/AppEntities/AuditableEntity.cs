using System;

namespace DagAir.Addresses.Data.AppEntities
{
    public abstract class AuditableEntity
    {
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}