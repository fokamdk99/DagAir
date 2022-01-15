using System.Threading.Tasks;
using DagAir.Sensors.Data.AppEntities;

namespace DagAir.Sensors.Sensors.Queries
{
    public interface IGetSensorByRoomId
    {
        Task<Sensor> Execute(long roomId);
    }
}