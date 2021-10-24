﻿using System.Collections.Generic;

namespace DagAir.Policies.Data.AppEntities
{
    public class RoomPolicyCategory : AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<RoomPolicy> RoomPolicies { get; set; }
    }
}