using DagAir.Facilities.Contracts.DTOs;

namespace DagAir.Facilities.Contracts.Commands
{
    public class AddNewAffiliateCommand : ICommand
    {
        public AffiliateDto AffiliateDto { get; set; }
    }
}