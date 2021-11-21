using DagAir.Policies.Contracts.DTOs;

namespace DagAir.Policies.Contracts.Commands
{
    public record AddNewRoomPolicyCommand : ICommand
    {
        public RoomPolicyDto RoomPolicyDto { get; set; }
        public ExpectedRoomConditionsDto ExpectedRoomConditionsDto { get; set; }
        public RoomPolicyCategoryDto RoomPolicyCategoryDto { get; set; }
    }
}