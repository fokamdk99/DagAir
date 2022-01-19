using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Contracts.DTOs;

namespace DagAir.AdminNode.Facilities
{
    public interface IFacilitiesHandler
    {
        Task<List<OrganizationDto>> GetOrganizations();
        Task<OrganizationDto> GetOrganizationById(long organizationId);
        Task<OrganizationDto> AddNewOrganization(AddNewOrganizationCommand addNewAddressCommand);
        Task<int> DeleteOrganization(long organizationId);
    }
}