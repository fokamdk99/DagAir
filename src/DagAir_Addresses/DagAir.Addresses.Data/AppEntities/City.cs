using System.Collections.Generic;

namespace DagAir.Addresses.Data.AppEntities
{
    public class City : AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}