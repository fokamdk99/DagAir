using System.Threading.Tasks;
using AutoMapper;
using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Data.AppContext;
using DagAir.Facilities.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DagAir.Facilities.Affiliates.Commands
{
    public class AddNewAffiliateCommandHandler : ICommandHandler<AddNewAffiliateCommand, Affiliate>
    {
        private readonly IDagAirFacilitiesAppContext _context;
        private readonly ILogger<AddNewAffiliateCommandHandler> _logger;
        private readonly IMapper _mapper;

        public AddNewAffiliateCommandHandler(IDagAirFacilitiesAppContext context, 
            IMapper mapper, 
            ILogger<AddNewAffiliateCommandHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Affiliate> Handle(AddNewAffiliateCommand command)
        {
            var affiliate = _mapper.Map<Affiliate>(command.AffiliateDto);
            
            var foundAffiliate =
                await _context.Affiliates.SingleOrDefaultAsync(x => x.Name == command.AffiliateDto.Name);

            
            if (foundAffiliate != null)
            {
                string message =
                    $"Error while creating new affiliate. Affiliate with name ${command.AffiliateDto.Name} already exists";
                _logger.LogError(message);
                return null;
            }
            
            await _context.Affiliates.AddAsync(affiliate);
            await _context.SaveChangesAsync();
            return affiliate;
        }
    }
}