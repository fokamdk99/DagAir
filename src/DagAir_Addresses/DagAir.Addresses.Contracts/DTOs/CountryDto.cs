using System.Collections.Generic;

namespace DagAir.Addresses.Contracts.DTOs
{
    public class CountryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<AddressDto> Addresses { get; set; }
    }
}