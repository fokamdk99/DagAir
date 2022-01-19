using System.Linq;
using System.Threading.Tasks;
using DagAir.Facilities.Data.AppContext;
using DagAir.Facilities.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Facilities.Rooms.Commands
{
    public class DeleteRoomHandler : IDeleteRoomHandler
    {
        private readonly IDagAirFacilitiesAppContext _context;

        public DeleteRoomHandler(IDagAirFacilitiesAppContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(long roomId)
        {
            var room = new Room {Id = roomId};
            _context.Rooms.Attach(room);
            _context.Rooms.Remove(room);
            bool saveFailed;
            int affectedRows = -1;
            do
            {
                saveFailed = false;
                try
                {
                    affectedRows = await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;
                    
                    var entry = ex.Entries.Single();
                    if (entry.State == EntityState.Deleted)
                    {
                        entry.State = EntityState.Detached;
                    }
                    else
                    {
                        entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    }
                }
            } while (saveFailed);
            return affectedRows;
        }
    }
}