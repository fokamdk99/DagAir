using System.Linq;
using System.Threading.Tasks;
using DagAir.QueryNode.Data.AppContext;
using DagAir.QueryNode.Rooms.Models;
using Microsoft.EntityFrameworkCore;

namespace DagAir.QueryNode.Rooms.Queries
{
    public class GetCurrentRoomQuery : IGetCurrentRoom
    {
        private readonly IDagAirAppContext _context;

        public GetCurrentRoomQuery(IDagAirAppContext context)
        {
            _context = context;
        }
        
        public async Task<CurrentRoomReadModel> Execute(long id)
        {
            var currentRoom = await _context.Rooms.Where(x => x.Id == id)
                .Select(x =>
                    new CurrentRoomReadModel(x.Id, x.Number, x.Floor, x.Affiliate.Id, x.Affiliate.Organization.Id))
                .SingleOrDefaultAsync();

            return currentRoom;
        }
    }
}