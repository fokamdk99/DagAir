using System.Threading.Tasks;
using DagAir.Sensors.Contracts.DTOs;

namespace DagAir.PolicyNode.Integrations.Sensors.DataServices
{
    public interface ISensorsDataService
    {
        Task<SensorDto> GetSensorBySensorName(string sensorName);
    }
}