using System.Threading.Tasks;
using DagAir.Sensors.Contracts.DTOs;

namespace DagAir.AdminNode.Sensors
{
    public interface ISensorsHandler
    {
        Task<SensorDto> GetSensorByRoomId(long roomId);
    }
}