using System.Threading.Tasks;
using DagAir.Facilities.Data.AppContext;
using DagAir.Facilities.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Facilities.Organizations.Queries
{
    public class GetOrganizationQuery : IGetOrganizationQuery
    {
        private readonly IDagAirFacilitiesAppContext _context;

        public GetOrganizationQuery(IDagAirFacilitiesAppContext context)
        {
            _context = context;
        }

        public async Task<Organization> Execute(long id)
        {
            var organization = await _context.Organizations.SingleOrDefaultAsync(x => x.Id == id);

            return organization;
        }
    }
}