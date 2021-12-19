using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DagAir.Facilities.Affiliates.Queries;
using DagAir.Facilities.Data.AppContext;
using DagAir.Facilities.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Facilities.Organizations.Queries
{
    public class GetOrganizationsQuery : IGetOrganizationsQuery
    {
        private readonly IDagAirFacilitiesAppContext _context;
        private readonly IGetAffiliatesByOrganizationQuery _getAffiliatesByOrganizationQuery;

        public GetOrganizationsQuery(IDagAirFacilitiesAppContext context, IGetAffiliatesByOrganizationQuery getAffiliatesByOrganizationQuery)
        {
            _context = context;
            _getAffiliatesByOrganizationQuery = getAffiliatesByOrganizationQuery;
        }

        public async Task<ICollection<Organization>> Execute()
        {
            var organizations = await _context.Organizations.ToListAsync();
            var organizationsTasks = organizations.Select(async x =>
            {
                x.Affiliates = await _context.Affiliates.Where(e => e.OrganizationId == x.Id).ToListAsync();
                return x;
            });

            var organizationsWithAffiliates = await Task.WhenAll(organizationsTasks);

            return organizationsWithAffiliates.ToList();
        }
    }
}