using System.Threading.Tasks;
using AutoMapper;
using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Data.AppContext;
using DagAir.Facilities.Data.AppEntitities;

namespace DagAir.Facilities.Affiliates.Commands
{
    public class AddNewAffiliateCommandHandler : ICommandHandler<AddNewAffiliateCommand, Affiliate>
    {
        private readonly IDagAirFacilitiesAppContext _context;
        private readonly IMapper _mapper;

        public AddNewAffiliateCommandHandler(IDagAirFacilitiesAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Affiliate> Handle(AddNewAffiliateCommand command)
        {
            var affiliate = _mapper.Map<Affiliate>(command.AffiliateDto);
            await _context.Affiliates.AddAsync(affiliate);
            await _context.SaveChangesAsync();
            return affiliate;
        }
    }
}