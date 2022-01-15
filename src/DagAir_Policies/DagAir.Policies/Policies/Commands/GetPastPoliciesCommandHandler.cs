using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.DataServices.SensorStateHistory.Contracts.Contracts;
using DagAir.Policies.Contracts.Commands;
using DagAir.Policies.Data.AppEntities;
using DagAir.Policies.Policies.Queries;

namespace DagAir.Policies.Policies.Commands
{
    
    public class GetPastPoliciesCommandHandler : ICommandHandler<GetPastPoliciesCommand, PastMeasurements>
    {
        private readonly IGetRoomPolicyQuery _getRoomPolicyQuery;

        public GetPastPoliciesCommandHandler(IGetRoomPolicyQuery getRoomPolicyQuery)
        {
            _getRoomPolicyQuery = getRoomPolicyQuery;
        }

        public async Task<PastMeasurements> Handle(GetPastPoliciesCommand getPastPoliciesCommand)
        {
            RoomPolicy currentRoomPolicy;
            var pastMeasurements = new PastMeasurements();
            pastMeasurements.Measurements = new List<Tuple<HistoricMeasurement, RoomPolicy>>();

            foreach (var historicMeasurement in getPastPoliciesCommand.HistoricMeasurements)
            {
                currentRoomPolicy = 
                    await _getRoomPolicyQuery.Handle(getPastPoliciesCommand.RoomId, historicMeasurement.Date);
                pastMeasurements.Measurements
                    .Add(new Tuple<HistoricMeasurement, RoomPolicy>(historicMeasurement, currentRoomPolicy));
            }

            return pastMeasurements;
        }
    }
    
}