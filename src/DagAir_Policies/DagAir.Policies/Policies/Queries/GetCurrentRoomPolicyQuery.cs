using System;
using System.Linq;
using System.Threading.Tasks;
using DagAir.Policies.Data.AppContext;
using DagAir.Policies.Data.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DagAir.Policies.Policies.Queries
{
    public class GetCurrentRoomPolicyQuery : IGetCurrentRoomPolicyQuery
    {
        private readonly IDagAirPoliciesAppContext _context;
        private readonly ILogger<GetCurrentRoomPolicyQuery> _logger;

        public GetCurrentRoomPolicyQuery(IDagAirPoliciesAppContext context,
            ILogger<GetCurrentRoomPolicyQuery> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task<RoomPolicy> Handle(long roomId)
        {
            var roomPolicies = await _context.RoomPolicies
                .Where(x => x.RoomId == roomId)
                .Include(x => x.Category)
                .Include(x => x.ExpectedConditions)
                .ToListAsync();

            var currentTime = DateTime.Now;
            var todaysPolicies = roomPolicies.Where(x =>
                String.IsNullOrEmpty(x.RepeatOn) || x.RepeatOn.Contains(currentTime.ToString("ddd")));
            var currentTimePolicies = todaysPolicies.Where(x => x.StartDate.Hour < currentTime.Hour && x.EndDate.Hour > currentTime.Hour);
            var orderedPolicies = currentTimePolicies.OrderByDescending(x => x.Category.CategoryNumber);
            try
            {
                return orderedPolicies.First();
            }
            catch (Exception e)
            {
                _logger.LogError($"No valid policy for roomId {roomId} has been found. Details: {e.Message}");
                return null;
            }
        }
    }
}