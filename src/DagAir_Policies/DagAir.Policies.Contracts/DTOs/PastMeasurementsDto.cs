using System;
using System.Collections.Generic;
using DagAir.DataServices.SensorStateHistory.Contracts.Contracts;

namespace DagAir.Policies.Contracts.DTOs
{
    public class PastMeasurementsDto
    {
        public List<Tuple<HistoricMeasurement, RoomPolicyDto>> Measurements { get; set; }
    }
}