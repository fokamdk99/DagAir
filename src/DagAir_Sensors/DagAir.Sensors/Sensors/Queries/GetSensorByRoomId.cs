using System.Threading.Tasks;
using DagAir.Sensors.Data.AppContext;
using DagAir.Sensors.Data.AppEntities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Sensors.Sensors.Queries
{
    public class GetSensorByRoomId : IGetSensorByRoomId
    {
        private readonly IDagAirSensorAppContext _context;

        public GetSensorByRoomId(IDagAirSensorAppContext context)
        {
            _context = context;
        }

        public async Task<Sensor> Execute(long roomId)
        {
            var sensor = await _context.Sensors.SingleOrDefaultAsync(x => x.RoomId == roomId);
            return sensor;
        }
    }
}