using System.Threading.Tasks;
using DagAir.Sensors.Data.AppContext;
using DagAir.Sensors.Data.AppEntities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Sensors.Sensors.Queries
{
    public class GetSensorBySensorName : IGetSensorBySensorName
    {
        private readonly IDagAirSensorAppContext _context;

        public GetSensorBySensorName(IDagAirSensorAppContext context)
        {
            _context = context;
        }

        public async Task<Sensor> Execute(string sensorName)
        {
            var sensor = await _context.Sensors.SingleOrDefaultAsync(x => x.SensorName == sensorName);
            return sensor;
        }
    }
}