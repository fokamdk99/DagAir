using System.Threading.Tasks;
using DagAir.Facilities.Contracts.DTOs;
using DagAir.WebAdminApp.Controllers;

namespace DagAir.WebAdminApp.Rooms
{
    public interface IRoomsHandler
    {
        Task<RoomDto> AddNewRoom(UniqueRoomModel uniqueRoomModel);
    }
}