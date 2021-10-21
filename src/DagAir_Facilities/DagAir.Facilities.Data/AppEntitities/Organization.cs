using System.Collections.Generic;

namespace DagAir.Facilities.Data.AppEntitities
{
    public class Organization : AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long AddressId { get; set; }
        public virtual ICollection<Affiliate> Affiliates { get; set; }
    }
}