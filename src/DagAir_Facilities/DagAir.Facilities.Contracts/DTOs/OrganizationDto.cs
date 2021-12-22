using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DagAir.Facilities.Contracts.DTOs
{
    public class OrganizationDto
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Organization name is required")]
        public string Name { get; set; }
        public long AddressId { get; set; }
        public virtual List<AffiliateDto> Affiliates { get; set; }
    }
}