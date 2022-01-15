using System.Threading.Tasks;
using DagAir.AdminNode.Contracts.Commands;
using DagAir.Policies.Contracts.DTOs;

namespace DagAir.AdminNode.SensorStateHistory
{
    public interface ISensorStateHistoryHandler
    {
        Task<PastMeasurementsDto> GetHistoricMeasurements(GetRoomCommand getRoomCommand);
    }
}