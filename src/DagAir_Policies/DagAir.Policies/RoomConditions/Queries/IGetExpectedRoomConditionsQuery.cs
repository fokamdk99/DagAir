using System.Threading.Tasks;
using DagAir.Policies.Data.AppEntities;

namespace DagAir.Policies.RoomConditions.Queries
{
    public interface IGetExpectedRoomConditionsQuery
    {
        Task<ExpectedRoomConditions> Handle(long id);
    }
}