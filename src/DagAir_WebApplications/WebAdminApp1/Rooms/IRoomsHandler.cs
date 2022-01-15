using System.Threading.Tasks;
using DagAir.AdminNode.Contracts.DTOs;
using DagAir.Facilities.Contracts.DTOs;
using WebAdminApp1.Rooms.Models;

namespace WebAdminApp1.Rooms
{
    public interface IRoomsHandler
    {
        Task<RoomDto> AddNewRoom(RoomModel uniqueRoomModel);
        Task<AdminNodeRoomDto> GetRoom(long roomId);
    }
}