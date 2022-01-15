using System;
using System.Collections.Generic;
using DagAir.DataServices.SensorStateHistory.Contracts.Contracts;
using DagAir.Policies.Data.AppEntities;

namespace DagAir.Policies.Policies
{
    public class PastMeasurements
    {
        public List<Tuple<HistoricMeasurement, RoomPolicy>> Measurements { get; set; }
    }
}