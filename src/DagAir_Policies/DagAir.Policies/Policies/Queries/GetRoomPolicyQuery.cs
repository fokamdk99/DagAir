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
                .ToListAsync();

            var roomPolicy = await _getPolicyHandler.GetRoomPolicy(roomPolicies, time);

            string message;
            if (roomPolicy == null)
            {
                var defaultPolicy = await _context.RoomPolicies.SingleAsync(x => x.CategoryId == 1);
                defaultPolicy.ExpectedConditions =
                    await _context.ExpectedRoomConditions.SingleOrDefaultAsync(x =>
                        x.Id == defaultPolicy.ExpectedConditionsId);
                defaultPolicy.Category =
                    await _context.RoomPolicyCategories.SingleOrDefaultAsync(x =>
                        x.Id == defaultPolicy.CategoryId);
                message = $"No current room policy found, applying default policy: {defaultPolicy}";
                _logger.LogInformation(message);
                return defaultPolicy;
            }

            message = $"Current room policy found: {roomPolicy}";
            _logger.LogInformation(message);
            
            roomPolicy.ExpectedConditions = await _context.ExpectedRoomConditions.SingleOrDefaultAsync(x =>
                x.Id == roomPolicy.ExpectedConditionsId);
            roomPolicy.Category =
                await _context.RoomPolicyCategories.SingleOrDefaultAsync(x =>
                    x.Id == roomPolicy.CategoryId);
            
            return roomPolicy;
        }

        
    }
}