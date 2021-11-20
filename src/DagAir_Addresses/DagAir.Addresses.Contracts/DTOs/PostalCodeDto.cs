using System.Collections.Generic;

namespace DagAir.Addresses.Contracts.DTOs
{
    public class PostalCodeDto
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public List<AddressDto> Addresses { get; set; }
    }
}