using System.Threading.Tasks;
using DagAir.Facilities.Affiliates.Queries;
using DagAir.Facilities.Data.AppContext;
using DagAir.Facilities.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Facilities.Organizations.Queries
{
    public class GetOrganizationQueryById : IGetOrganizationQueryById
    {
        private readonly IDagAirFacilitiesAppContext _context;
        private readonly IGetAffiliatesByOrganizationQuery _getAffiliatesByOrganizationQuery;

        public GetOrganizationQueryById(IDagAirFacilitiesAppContext context, IGetAffiliatesByOrganizationQuery getAffiliatesByOrganizationQuery)
        {
            _context = context;
            _getAffiliatesByOrganizationQuery = getAffiliatesByOrganizationQuery;
        }

        public async Task<Organization> Execute(long organizationId)
        {
            var organization = await _context.Organizations.SingleOrDefaultAsync(x => x.Id == organizationId);
            organization.Affiliates = await _getAffiliatesByOrganizationQuery.Execute(organization.Id);

            return organization;
        }
    }
}