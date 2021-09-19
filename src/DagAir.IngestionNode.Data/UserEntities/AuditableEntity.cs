using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DagAir.IngestionNode.Data.UserEntities
{
    public class AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}