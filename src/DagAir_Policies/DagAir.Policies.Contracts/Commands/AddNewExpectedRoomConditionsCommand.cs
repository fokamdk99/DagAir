using DagAir.Policies.Contracts.DTOs;

namespace DagAir.Policies.Contracts.Commands
{
    public record AddNewExpectedRoomConditionsCommand : ICommand
    {
        public ExpectedRoomConditionsDto ExpectedRoomConditionsDto { get; set; }
    }
}