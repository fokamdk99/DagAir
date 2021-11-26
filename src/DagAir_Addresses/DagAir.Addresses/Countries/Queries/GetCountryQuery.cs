using System.Threading.Tasks;
using DagAir.Addresses.Data.AppContext;
using DagAir.Addresses.Data.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DagAir.Addresses.Countries.Queries
{
    public class GetCountryQuery : IGetCountryQuery
    {
        private readonly IDagAirAddressesAppContext _context;
        private readonly ILogger<GetCountryQuery> _logger;

        public GetCountryQuery(IDagAirAddressesAppContext context,
            ILogger<GetCountryQuery> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task<Country> Handle(long countryId)
        {
            var country = await _context.Countries.SingleOrDefaultAsync(x => x.Id == countryId);

            return country;
        }
    }
}