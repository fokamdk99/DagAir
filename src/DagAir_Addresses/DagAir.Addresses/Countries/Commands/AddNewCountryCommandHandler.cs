using System.Threading.Tasks;
using AutoMapper;
using DagAir.Addresses.Contracts.Commands;
using DagAir.Addresses.Data.AppContext;
using DagAir.Addresses.Data.AppEntities;

namespace DagAir.Addresses.Addresses.Commands
{
    public class AddNewCountryCommandHandler : ICommandHandler<AddNewCountryCommand, Country>
    {
        private readonly IDagAirAddressesAppContext _context;
        private readonly IMapper _mapper;

        public AddNewCountryCommandHandler(IDagAirAddressesAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Country> Handle(AddNewCountryCommand command)
        {
            var country = _mapper.Map<Country>(command.CountryDto);
            await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();
            return country;
        }
    }
}