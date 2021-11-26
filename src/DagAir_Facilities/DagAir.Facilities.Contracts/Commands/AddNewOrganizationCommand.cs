using DagAir.Facilities.Contracts.DTOs;

namespace DagAir.Facilities.Contracts.Commands
{
    public class AddNewOrganizationCommand : ICommand
    {
        public OrganizationDto OrganizationDto { get; set; }
    }
}