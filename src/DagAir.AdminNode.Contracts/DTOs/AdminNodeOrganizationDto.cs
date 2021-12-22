using DagAir.Addresses.Contracts.DTOs;
using DagAir.Facilities.Contracts.DTOs;

namespace DagAir.AdminNode.Contracts.DTOs
{
    public class AdminNodeOrganizationDto
    {
        public OrganizationDto OrganizationDto { get; set; }
        public AddressDto AddressDto { get; set; }
    }
}