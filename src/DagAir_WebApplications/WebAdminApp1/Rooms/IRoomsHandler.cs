using System.Threading.Tasks;
using DagAir.Facilities.Contracts.DTOs;
using WebAdminApp1.Controllers;

namespace WebAdminApp1.Rooms
{
    public interface IRoomsHandler
    {
        Task<RoomDto> AddNewRoom(UniqueRoomModel uniqueRoomModel);
    }
}