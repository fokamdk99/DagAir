using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.Facilities.Contracts.DTOs;

namespace DagAir.WebAdminApp.Facilities
{
    public interface IFacilitiesHandler
    {
        Task<List<OrganizationDto>> GetOrganizations();
    }
}