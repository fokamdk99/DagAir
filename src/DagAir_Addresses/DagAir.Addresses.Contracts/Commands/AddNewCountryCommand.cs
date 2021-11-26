using DagAir.Addresses.Contracts.DTOs;

namespace DagAir.Addresses.Contracts.Commands
{
    public class AddNewCountryCommand : ICommand
    {
        public CountryDto CountryDto { get; set; }
    }
}