using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.Sensors.Data.AppContext;
using DagAir.Sensors.Data.AppEntities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Sensors.Sensors.Queries
{
    public class GetAllSensorsWithRelatedEntitiesQuery : IGetAllSensorsWithRelatedEntitiesQuery
    {
        private readonly IDagAirSensorAppContext _context;

        public GetAllSensorsWithRelatedEntitiesQuery(IDagAirSensorAppContext context)
        {
            _context = context;
        }
        
        public async Task<List<Sensor>> Execute()
        {
            return await _context.Sensors.ToListAsync();
        }
    }
}