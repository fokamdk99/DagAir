using System.Collections.Generic;

namespace DagAir.Facilities.Contracts.DTOs
{
    public class OrganizationDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long AddressId { get; set; }
        public virtual List<AffiliateDto> Affiliates { get; set; }
    }
}