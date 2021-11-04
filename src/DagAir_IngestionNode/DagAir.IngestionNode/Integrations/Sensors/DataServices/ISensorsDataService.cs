using System.Threading.Tasks;
using DagAir.Sensors.Contracts.DTOs;

namespace DagAir.IngestionNode.Integrations.Sensors.DataServices
{
    public interface ISensorsDataService
    {
        Task<SensorDto> GetSensorById(string id);
    }
}