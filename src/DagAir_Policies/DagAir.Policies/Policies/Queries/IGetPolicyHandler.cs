using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.Policies.Data.AppEntities;

namespace DagAir.Policies.Policies.Queries
{
    public interface IGetPolicyHandler
    {
        Task<RoomPolicy> GetRoomPolicy(IEnumerable<RoomPolicy> roomPolicies, DateTime time);
    }
}