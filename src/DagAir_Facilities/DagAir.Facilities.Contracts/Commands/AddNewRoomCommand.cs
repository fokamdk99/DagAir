using DagAir.Facilities.Contracts.DTOs;

namespace DagAir.Facilities.Contracts.Commands
{
    public class AddNewRoomCommand : ICommand
    {
        public RoomDto RoomDto { get; set; } 
    }
}