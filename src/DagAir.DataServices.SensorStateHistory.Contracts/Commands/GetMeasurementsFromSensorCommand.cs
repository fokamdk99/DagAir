using System;

namespace DagAir.DataServices.SensorStateHistory.Contracts.Commands
{
    public class GetMeasurementsFromSensorCommand : ICommand
    {
        public string SensorName { get; set; }
        public long RoomId { get; set; }
        public int NumberOfRecords { get; set; }
    }
}