using System.Threading.Tasks;
using DagAir.Facilities.Data.AppContext;
using DagAir.Facilities.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Facilities.Rooms.Queries
{
    public class GetCurrentRoomQuery : IGetCurrentRoomQuery
    {
        private readonly IDagAirFacilitiesAppContext _context;

        public GetCurrentRoomQuery(IDagAirFacilitiesAppContext context)
        {
            _context = context;
        }
        
        public async Task<Room> Execute(long id)
        {
            var currentRoom = await _context.Rooms.SingleOrDefaultAsync(x => x.Id == id);

            return currentRoom;
        }
    }
}