using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DagAir.Addresses.Contracts.DTOs
{
    public class PostalCodeDto
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Postal code is required")]
        public string Number { get; set; }
        public List<AddressDto> Addresses { get; set; }
    }
}