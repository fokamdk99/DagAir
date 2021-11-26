using System.Threading.Tasks;
using DagAir.Addresses.Data.AppContext;
using DagAir.Addresses.Data.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DagAir.Addresses.Cities.Queries
{
    public class GetCityQuery : IGetCityQuery
    {
        private readonly IDagAirAddressesAppContext _context;
        private readonly ILogger<GetCityQuery> _logger;

        public GetCityQuery(IDagAirAddressesAppContext context,
            ILogger<GetCityQuery> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task<City> Handle(long cityId)
        {
            var city = await _context.Cities.SingleOrDefaultAsync(x => x.Id == cityId);

            return city;
        }
    }
}