using System.Collections.Generic;

namespace DagAir.Policies.Contracts.DTOs
{
    public class RoomPolicyCategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<RoomPolicyDto> RoomPolicies { get; set; }
    }
}