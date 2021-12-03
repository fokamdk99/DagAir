using System.Threading.Tasks;
using DagAir.Facilities.Contracts.DTOs;

namespace DagAir.PolicyNode.Integrations.Facilities.DataServices
{
    public interface IFacilitiesDataService
    {
        Task<RoomDto> GetRoomByRoomId(long roomId);
    }
}