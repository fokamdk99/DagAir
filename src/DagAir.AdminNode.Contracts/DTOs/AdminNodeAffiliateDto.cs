using DagAir.Addresses.Contracts.DTOs;
using DagAir.Facilities.Contracts.DTOs;

namespace DagAir.AdminNode.Contracts.DTOs
{
    public class AdminNodeAffiliateDto
    {
        public AffiliateDto AffiliateDto { get; set; }
        public AddressDto AddressDto { get; set; }
    }
}