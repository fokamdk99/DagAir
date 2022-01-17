using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.DataServices.SensorStateHistory.Contracts.Commands;
using DagAir.DataServices.SensorStateHistory.Contracts.Contracts;

namespace DagAir.DataServices.SensorStateHistory.Measurements.Queries
{
    public interface IGetMeasurementsFromSensorQuery
    {
        Task<List<HistoricMeasurement>> GetMeasurementsFromSensor(GetMeasurementsFromSensorCommand getMeasurementsForAGivenRoomCommand);
    }
}