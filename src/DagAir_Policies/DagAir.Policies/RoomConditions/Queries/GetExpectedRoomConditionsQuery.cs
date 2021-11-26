using System.Threading.Tasks;
using DagAir.Policies.Data.AppContext;
using DagAir.Policies.Data.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DagAir.Policies.RoomConditions.Queries
{
    public class GetExpectedRoomConditionsQuery : IGetExpectedRoomConditionsQuery
    {
        private readonly IDagAirPoliciesAppContext _context;
        private readonly ILogger<GetExpectedRoomConditionsQuery> _logger;

        public GetExpectedRoomConditionsQuery(IDagAirPoliciesAppContext context, ILogger<GetExpectedRoomConditionsQuery> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ExpectedRoomConditions> Handle(long id)
        {
            var expectedRoomConditions = await _context.ExpectedRoomConditions.SingleOrDefaultAsync(x => x.Id == id);
            return expectedRoomConditions;
        }
    }
}