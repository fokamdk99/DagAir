using DagAir.Addresses.Contracts.DTOs;

namespace DagAir.Addresses.Contracts.Commands
{
    public class AddNewPostalCodeCommand : ICommand
    {
        public PostalCodeDto PostalCodeDto { get; set; }
    }
}