using System.Threading.Tasks;
using DagAir.Sensors.Data.AppContext;
using DagAir.Sensors.Data.AppEntities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Sensors.Sensors.Queries
{
    public class GetSensorWithRelatedEntitiesQuery : IGetSensorWithRelatedEntitiesQuery
    {
        private readonly IDagAirSensorAppContext _context;

        public GetSensorWithRelatedEntitiesQuery(IDagAirSensorAppContext context)
        {
            _context = context;
        }
        
        public async Task<Sensor> Execute(long id)
        {
            return await _context.Sensors.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}