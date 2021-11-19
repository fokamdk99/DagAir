using System.Linq;
using System.Threading.Tasks;
using DagAir.Facilities.Data.AppContext;
using DagAir.Facilities.Rooms.Models;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Facilities.Rooms.Queries
{
    public class GetCurrentRoomQuery : IGetCurrentRoom
    {
        private readonly IDagAirFacilitiesAppContext _context;

        public GetCurrentRoomQuery(IDagAirFacilitiesAppContext context)
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