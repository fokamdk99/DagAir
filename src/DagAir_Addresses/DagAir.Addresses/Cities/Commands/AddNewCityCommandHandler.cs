using System.Threading.Tasks;
using AutoMapper;
using DagAir.Addresses.Contracts.Commands;
using DagAir.Addresses.Data.AppContext;
using DagAir.Addresses.Data.AppEntities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Addresses.Addresses.Commands
{
    public class AddNewCityCommandHandler : ICommandHandler<AddNewCityCommand, City>
    {
        private readonly IDagAirAddressesAppContext _context;
        private readonly IMapper _mapper;

        public AddNewCityCommandHandler(IDagAirAddressesAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<City> Handle(AddNewCityCommand command)
        {
            var city = _mapper.Map<City>(command.CityDto);
            var foundCity = await _context.Cities.SingleOrDefaultAsync(x => x.Name == city.Name);
            if (foundCity != null)
            {
                return foundCity;
            }
            
            await _context.Cities.AddAsync(city);
            await _context.SaveChangesAsync();
            return city;
        }
    }
}