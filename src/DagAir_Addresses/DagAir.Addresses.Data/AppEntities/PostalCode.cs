using System.Collections.Generic;

namespace DagAir.Addresses.Data.AppEntities
{
    public class PostalCode : AuditableEntity
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}