using System.Threading.Tasks;
using DagAir.AdminNode.Contracts.Commands;
using DagAir.AdminNode.Contracts.DTOs;
using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Contracts.DTOs;

namespace DagAir.AdminNode.Rooms
{
    public interface IRoomsHandler
    {
        Task<AdminNodeRoomDto> GetRoomByRoomId(GetRoomCommand getRoomCommand);
        Task<RoomDto> AddNewRoom(AddNewRoomCommand addNewRoomCommand);
        Task<int> DeleteRoom(long roomId);

    }
}