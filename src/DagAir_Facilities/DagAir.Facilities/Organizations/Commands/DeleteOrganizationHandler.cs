using System.Linq;
using System.Threading.Tasks;
using DagAir.Facilities.Data.AppContext;
using DagAir.Facilities.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Facilities.Organizations.Commands
{
    public class DeleteOrganizationHandler : IDeleteOrganizationHandler
    {
        private readonly IDagAirFacilitiesAppContext _context;

        public DeleteOrganizationHandler(IDagAirFacilitiesAppContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(long organizationId)
        {
            var organization = new Organization {Id = organizationId};
            _context.Organizations.Attach(organization);
            _context.Organizations.Remove(organization);
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