using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.Facilities.Data.AppContext;
using DagAir.Facilities.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Facilities.Affiliates.Queries
{
    public class GetAffiliatesQuery : IGetAffiliatesQuery
    {
        private readonly IDagAirFacilitiesAppContext _context;

        public GetAffiliatesQuery(IDagAirFacilitiesAppContext context)
        {
            _context = context;
        }

        public async Task<List<Affiliate>> Execute()
        {
            var affiliates = await _context.Affiliates.ToListAsync();

            return affiliates;
        }
    }
}