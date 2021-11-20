using System.Linq;
using System.Threading.Tasks;
using DagAir.Addresses.Data.AppContext;
using DagAir.Addresses.Data.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DagAir.Addresses.Addresses.Queries
{
    public class GetAddressQuery : IGetAddressQuery
    {
        private readonly IDagAirAddressesAppContext _context;
        private readonly ILogger<GetAddressQuery> _logger;

        public GetAddressQuery(IDagAirAddressesAppContext context,
            ILogger<GetAddressQuery> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task<Address> Handle(long addressId)
        {
            var address = await _context.Addresses.SingleOrDefaultAsync(x => x.Id == addressId);
            address.Country = await _context.Countries.SingleOrDefaultAsync(x => x.Id == address.CountryId);
            address.City = await _context.Cities.SingleOrDefaultAsync(x => x.Id == address.CityId);
            address.PostalCode = await _context.PostalCodes.SingleOrDefaultAsync(x => x.Id == address.PostalCodeId);
            
            return address;
        }
    }
}