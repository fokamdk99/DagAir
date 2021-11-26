using System.Threading.Tasks;
using AutoMapper;
using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Data.AppContext;
using DagAir.Facilities.Data.AppEntitities;

namespace DagAir.Facilities.Organizations.Commands
{
    public class AddNewOrganizationCommandHandler : ICommandHandler<AddNewOrganizationCommand, Organization>
    {
        private readonly IDagAirFacilitiesAppContext _context;
        private readonly IMapper _mapper;

        public AddNewOrganizationCommandHandler(IDagAirFacilitiesAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Organization> Handle(AddNewOrganizationCommand command)
        {
            var organization = _mapper.Map<Organization>(command.OrganizationDto);
            await _context.Organizations.AddAsync(organization);
            await _context.SaveChangesAsync();
            return organization;
        }
    }
}