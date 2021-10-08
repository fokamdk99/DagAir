using System.Collections.Generic;

namespace DagAir.QueryNode.Data.AppEntitities
{
    public class Affiliate : AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ICollection<Sensor> Sensors { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}