using System.Threading.Tasks;
using DagAir.Policies.Contracts.DTOs;

namespace DagAir.PolicyNode.Integrations.Policies.DataServices
{
    public interface IPoliciesDataService
    {
        Task<RoomPolicyDto> GetRoomPolicyByRoomId(long roomId);
    }
}