using System.Linq;
using System.Threading.Tasks;
using DagAir.Facilities.Data.AppContext;
using DagAir.Facilities.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Facilities.Affiliates.Commands
{
    public class DeleteAffiliateHandler : IDeleteAffiliateHandler
    {
        private readonly IDagAirFacilitiesAppContext _context;

        public DeleteAffiliateHandler(IDagAirFacilitiesAppContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(long affiliateId)
        {
            var affiliate = new Affiliate {Id = affiliateId};
            _context.Affiliates.Attach(affiliate);
            _context.Affiliates.Remove(affiliate);
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