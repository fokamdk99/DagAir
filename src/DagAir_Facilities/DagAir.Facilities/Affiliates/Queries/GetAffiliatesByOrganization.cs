using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DagAir.Facilities.Data.AppContext;
using DagAir.Facilities.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Facilities.Affiliates.Queries
{
    public class GetAffiliatesByOrganizationQuery : IGetAffiliatesByOrganizationQuery
    {
        private readonly IDagAirFacilitiesAppContext _context;

        public GetAffiliatesByOrganizationQuery(IDagAirFacilitiesAppContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Affiliate>> Execute(long organizationId)
        {
            var affiliates = await _context.Affiliates.Where(x => x.OrganizationId == organizationId).ToListAsync();
            return affiliates;
        }
    }
}