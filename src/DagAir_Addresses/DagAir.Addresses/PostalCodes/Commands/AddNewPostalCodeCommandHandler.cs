using System.Threading.Tasks;
using AutoMapper;
using DagAir.Addresses.Contracts.Commands;
using DagAir.Addresses.Data.AppContext;
using DagAir.Addresses.Data.AppEntities;
using Microsoft.EntityFrameworkCore;

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
            var foundPostalCode = await _context.PostalCodes.SingleOrDefaultAsync(x => x.Number == postalCode.Number);
            if (foundPostalCode != null)
            {
                return foundPostalCode;
            }
            await _context.PostalCodes.AddAsync(postalCode);
            await _context.SaveChangesAsync();
            return postalCode;
        }
    }
}