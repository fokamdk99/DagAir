using System;

namespace DagAir.PolicyNode.Data.AppEntities
{
    public abstract class AuditableEntity
    {
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}