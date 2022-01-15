using System;
using System.Collections.Generic;

namespace DagAir.Facilities.Data.AppEntitities
{
    public class Room : AuditableEntity
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public int Floor { get; set; }
        public long AffiliateId { get; set; }
        public virtual Affiliate Affiliate { get; set; }
    }
}