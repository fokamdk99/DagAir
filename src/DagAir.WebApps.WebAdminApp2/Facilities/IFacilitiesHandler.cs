using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.Facilities.Contracts.DTOs;

namespace DagAir.WebApps.WebAdminApp2.Facilities
{
    public interface IFacilitiesHandler
    {
        Task<List<OrganizationDto>> GetOrganizations();
    }
}