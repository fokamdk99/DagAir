using System;

namespace DagAir.Policies.Contracts.DTOs
{
    public class RoomPolicyDto
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string RepeatOn { get; set; }
        public long ExpectedConditionsId { get; set; } 
        public long CategoryId { get; set; }
        public long RoomId { get; set; }
        public ExpectedRoomConditionsDto ExpectedConditions { get; set; }
        public RoomPolicyCategoryDto Category { get; set; }
    }
}