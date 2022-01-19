using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.AdminNode.Contracts.DTOs;
using DagAir.Facilities.Contracts.DTOs;
using WebAdminApp1.Controllers;
using WebAdminApp1.Facilities.Models;

namespace WebAdminApp1.Facilities
{
    public interface IFacilitiesHandler
    {
        Task<List<AdminNodeOrganizationDto>> GetOrganizations();
        Task<AdminNodeOrganizationDto> GetOrganization(long organizationId);
        Task<OrganizationDto> AddNewOrganization(OrganizationModel addNewOrganizationCommand);
        Task<int> DeleteOrganization(long organizationId);
    }
}