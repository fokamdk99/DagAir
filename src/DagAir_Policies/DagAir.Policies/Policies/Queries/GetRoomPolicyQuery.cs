using System;
using System.Linq;
using System.Threading.Tasks;
using DagAir.Policies.Data.AppContext;
using DagAir.Policies.Data.AppEntities;
using MassTransit.Initializers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DagAir.Policies.Policies.Queries
{
    public class GetRoomPolicyQuery : IGetRoomPolicyQuery
    {
        private readonly IDagAirPoliciesAppContext _context;
        private readonly ILogger<GetRoomPolicyQuery> _logger;
        private readonly IGetPolicyHandler _getPolicyHandler;

        public GetRoomPolicyQuery(IDagAirPoliciesAppContext context,
            ILogger<GetRoomPolicyQuery> logger, 
            IGetPolicyHandler getPolicyHandler)
        {
            _context = context;
            _logger = logger;
            _getPolicyHandler = getPolicyHandler;
        }
        
        public async Task<RoomPolicy> Handle(long roomId, DateTime time)
        {
            var roomPolicies = await _context.RoomPolicies
                .Where(x => x.RoomId == roomId)
                .Include(x => x.Category)
                .Include(x => x.ExpectedConditions)
                .ToListAsync();

            var roomPolicy = await _getPolicyHandler.GetRoomPolicy(roomPolicies, time);

            string message;
            if (roomPolicy == null)
            {
                var defaultPolicy = await _context.RoomPolicies.Where(x => x.CategoryId == 1)
                    .Include(x => x.Category)
                    .Include(x => x.ExpectedConditions)
                    .SingleAsync();
                
                message = $"No current room policy found, applying default policy: {defaultPolicy}";
                _logger.LogInformation(message);
                return defaultPolicy;
            }

            message = $"Current room policy found: {roomPolicy}";
            _logger.LogInformation(message);
            
            return roomPolicy;
        }

        
    }
}