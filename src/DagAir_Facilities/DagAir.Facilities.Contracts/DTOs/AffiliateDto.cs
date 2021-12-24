using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DagAir.Facilities.Contracts.DTOs
{
    public class AffiliateDto
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Affiliate name is required")]
        public string Name { get; set; }
        public long AddressId { get; set; }
        public long OrganizationId { get; set; }
        public OrganizationDto Organization { get; set; }
        public List<RoomDto> Rooms { get; set; }
    }
}