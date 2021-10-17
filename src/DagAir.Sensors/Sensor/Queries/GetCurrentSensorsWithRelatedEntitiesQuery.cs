using System.Linq;
using System.Threading.Tasks;
using DagAir.Sensors.Data.AppContext;
using DagAir.Sensors.Sensor.Models;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Sensors.Sensor.Queries
{
    public class GetCurrentSensorsWithRelatedEntitiesQuery : IQuery<SensorReadModel>
    {
        private readonly IDagAirSensorAppContext _context;

        public GetCurrentSensorsWithRelatedEntitiesQuery(IDagAirSensorAppContext context)
        {
            _context = context;
        }
        
        public async Task<SensorReadModel> Execute(long id)
        {
            var sensor = await _context.Sensors.Where(x => x.Id == id).Include(x => x.SensorModel).SingleAsync();
            return new SensorReadModel(sensor.Id,
                sensor.LastDataSentDate,
                sensor.RoomId,
                sensor.AffiliateId,
                sensor.SensorModel);

        }
    }
}