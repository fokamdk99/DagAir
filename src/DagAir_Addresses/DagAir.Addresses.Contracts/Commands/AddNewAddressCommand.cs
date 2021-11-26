using DagAir.Addresses.Contracts.DTOs;

namespace DagAir.Addresses.Contracts.Commands
{
    public class AddNewAddressCommand : ICommand
    {
        public AddressDto AddressDto { get; set; }
        public CityDto CityDto { get; set; }
        public CountryDto CountryDto { get; set; }
        public PostalCodeDto PostalCodeDto { get; set; }
    }
}