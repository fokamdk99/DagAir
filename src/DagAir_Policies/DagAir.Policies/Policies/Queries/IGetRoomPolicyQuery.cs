using System;
using System.Threading.Tasks;
using DagAir.Policies.Data.AppEntities;

namespace DagAir.Policies.Policies.Queries
{
    public interface IGetRoomPolicyQuery
    {
        Task<RoomPolicy> Handle(long id, DateTime time);
    }
}