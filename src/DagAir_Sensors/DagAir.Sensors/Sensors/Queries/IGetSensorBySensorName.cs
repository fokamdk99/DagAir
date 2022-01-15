using System.Threading.Tasks;
using DagAir.Sensors.Data.AppEntities;

namespace DagAir.Sensors.Sensors.Queries
{
    public interface IGetSensorBySensorName
    {
        Task<Sensor> Execute(string sensorName);
    }
}