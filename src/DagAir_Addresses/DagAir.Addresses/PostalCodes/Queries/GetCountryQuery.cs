using System.Threading.Tasks;
using DagAir.Addresses.Data.AppContext;
using DagAir.Addresses.Data.AppEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DagAir.Addresses.PostalCodes.Queries
{
    public class GetPostalCodeQuery : IGetPostalCodeQuery
    {
        private readonly IDagAirAddressesAppContext _context;
        private readonly ILogger<GetPostalCodeQuery> _logger;

        public GetPostalCodeQuery(IDagAirAddressesAppContext context,
            ILogger<GetPostalCodeQuery> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task<PostalCode> Handle(long postalCodeId)
        {
            var postalCode = await _context.PostalCodes.SingleOrDefaultAsync(x => x.Id == postalCodeId);

            return postalCode;
        }
    }
}