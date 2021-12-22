using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.AdminNode.Contracts.DTOs;
using DagAir.Facilities.Contracts.DTOs;
using DagAir.WebAdminApp.Controllers;

namespace DagAir.WebAdminApp.Facilities
{
    public interface IFacilitiesHandler
    {
        Task<List<AdminNodeOrganizationDto>> GetOrganizations();
        Task<AdminNodeOrganizationDto> GetOrganization(long organizationId);
        Task<OrganizationDto> AddNewOrganization(GetOrganizationModel addNewOrganizationCommand);
    }
}