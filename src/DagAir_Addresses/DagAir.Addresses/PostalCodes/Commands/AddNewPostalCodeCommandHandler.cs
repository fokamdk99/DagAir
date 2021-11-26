using System.Threading.Tasks;
using AutoMapper;
using DagAir.Addresses.Contracts.Commands;
using DagAir.Addresses.Data.AppContext;
using DagAir.Addresses.Data.AppEntities;

namespace DagAir.Addresses.Addresses.Commands
{
    public class AddNewPostalCodeCommandHandler : ICommandHandler<AddNewPostalCodeCommand, PostalCode>
    {
        private readonly IDagAirAddressesAppContext _context;
        private readonly IMapper _mapper;

        public AddNewPostalCodeCommandHandler(IMapper mapper, IDagAirAddressesAppContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PostalCode> Handle(AddNewPostalCodeCommand command)
        {
            var postalCode = _mapper.Map<PostalCode>(command.PostalCodeDto);
            await _context.PostalCodes.AddAsync(postalCode);
            await _context.SaveChangesAsync();
            return postalCode;
        }
    }
}