using System.Collections.Generic;
using DagAir.DataServices.SensorStateHistory.Contracts.Contracts;

namespace DagAir.Policies.Contracts.Commands
{
    public class GetPastPoliciesCommand : ICommand
    {
        public long RoomId { get; set; }
        public List<HistoricMeasurement> HistoricMeasurements { get; set; }
    }
}