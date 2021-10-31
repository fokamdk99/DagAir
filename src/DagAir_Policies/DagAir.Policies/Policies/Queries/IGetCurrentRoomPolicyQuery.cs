using System.Threading.Tasks;
using DagAir.Policies.Data.AppEntities;

namespace DagAir.Policies.Policies.Queries
{
    public interface IGetCurrentRoomPolicyQuery
    {
        Task<RoomPolicy> Handle(long id);
    }
}