using System.Threading.Tasks;
using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Contracts.DTOs;

namespace DagAir.AdminNode.Rooms
{
    public interface IRoomsHandler
    {
        Task<RoomDto> AddNewRoom(AddNewRoomCommand addNewRoomCommand);
    }
}