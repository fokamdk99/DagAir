using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DagAir.Addresses.Contracts.DTOs
{
    public class CityDto
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "City name is required")]
        public string Name { get; set; }
        public List<AddressDto> Addresses { get; set; }
    }
}