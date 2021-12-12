using System.Linq;
using System.Threading.Tasks;
using DagAir.Facilities.Data.AppContext;
using DagAir.Facilities.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Facilities.Affiliates.Queries
{
    public class GetAffiliateQuery : IGetAffiliateQuery
    {
        private readonly IDagAirFacilitiesAppContext _context;

        public GetAffiliateQuery(IDagAirFacilitiesAppContext context)
        {
            _context = context;
        }

        public async Task<Affiliate> Execute(long id)
        {
            var affiliate = await _context.Affiliates
                .Include(x => x.Rooms)
                .Include(x => x.Organization)
                .SingleOrDefaultAsync(x => x.Id == id);

            return affiliate;
        }
    }
}