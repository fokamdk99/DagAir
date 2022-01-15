using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DagAir.Policies.Data.AppEntities;

namespace DagAir.Policies.Policies.Queries
{
    public class GetPolicyHandler : IGetPolicyHandler
    {
        public async Task<RoomPolicy> GetRoomPolicy(IEnumerable<RoomPolicy> roomPolicies, DateTime time)
        {
            var currentTime = time;

            var activePolicies = roomPolicies.Where(x =>
                x.StartDate <= currentTime && x.EndDate >= currentTime);

            var currentPolicies = activePolicies.Where(x =>
                x.StartHour <= currentTime.Hour && x.EndHour >= currentTime.Hour ||
                x.StartHour > x.EndHour && currentTime.Hour >= x.StartHour ||
                x.StartHour > x.EndHour && currentTime.Hour <= x.EndHour);
            
            var todaysPolicies = currentPolicies.Where(x =>
                String.IsNullOrEmpty(x.RepeatOn) || 
                x.RepeatOn.Contains(currentTime.ToString("ddd")) ||
                (x.SpansTwoDays && x.RepeatOn.Contains(currentTime.AddDays(-1).ToString("ddd"))));

            var orderedPolicies = todaysPolicies.OrderByDescending(x => x.Category.CategoryNumber);
            
            try
            {
                return orderedPolicies.First();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}