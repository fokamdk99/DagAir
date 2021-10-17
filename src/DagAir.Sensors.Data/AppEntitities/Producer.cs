﻿using System;
using System.Collections.Generic;

namespace DagAir.QueryNode.Data.AppEntitities
{
    public class Producer : AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfEstablishment { get; set; }
        public long AddressId { get; set; }
        public virtual ICollection<SensorModel> SensorModels { get; set; }
    }
}