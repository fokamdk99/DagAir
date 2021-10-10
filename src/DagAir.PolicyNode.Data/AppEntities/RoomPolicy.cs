using System;

namespace DagAir.PolicyNode.Data.AppEntities
{
    public class RoomPolicy : AuditableEntity
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string RepeatOn { get; set; }
        public long ExpectedConditionsId { get; set; } 
        public long CategoryId { get; set; }
        public long RoomId { get; set; }
        public virtual ExpectedRoomConditions ExpectedConditions { get; set; }
        public virtual RoomPolicyCategory Category { get; set; }
    }
}