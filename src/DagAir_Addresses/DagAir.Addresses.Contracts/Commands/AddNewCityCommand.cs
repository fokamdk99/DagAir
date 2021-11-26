using DagAir.Addresses.Contracts.DTOs;

namespace DagAir.Addresses.Contracts.Commands
{
    public class AddNewCityCommand : ICommand
    {
        public CityDto CityDto { get; set; }
    }
}