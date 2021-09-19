using System.Collections.Generic;
using DagAir.IngestionNode.Contracts;

namespace DagAir.IngestionNode.Data.UserEntities
{
    public class Room : AuditableEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public RoomLocation RoomLocation { get; set; }
    }
}