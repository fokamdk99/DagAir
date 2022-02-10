using System.Linq;
using System.Threading.Tasks;
using DagAir.AdminNode.Contracts.Commands;
using DagAir.AdminNode.Hubs;
using DagAir.AdminNode.SensorStateHistory;
using DagAir.PolicyNode.Contracts.Contracts;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace DagAir.AdminNode.Consumers
{
    public class PoliciesEvaluationResultEventConsumer : IConsumer<PoliciesEvaluationResultEvent>
    {
        private readonly ILogger<PoliciesEvaluationResultEventConsumer> _logger;
        private readonly IHubContext<ChatHub, IAdminNodeHub> _hubContext;
        private readonly ISensorStateHistoryHandler _sensorStateHistoryHandler;

        public PoliciesEvaluationResultEventConsumer(ISensorStateHistoryHandler sensorStateHistoryHandler)
        {
            _sensorStateHistoryHandler = sensorStateHistoryHandler;
        }

        public PoliciesEvaluationResultEventConsumer(ILogger<PoliciesEvaluationResultEventConsumer> logger,
            IHubContext<ChatHub, IAdminNodeHub> hubContext, 
            ISensorStateHistoryHandler sensorStateHistoryHandler)
        {
            _logger = logger;
            _hubContext = hubContext;
            _sensorStateHistoryHandler = sensorStateHistoryHandler;
        }
        public async Task Consume(ConsumeContext<PoliciesEvaluationResultEvent> context)
        {
            var policiesEvaluationResultEvent = context.Message;
            _logger.LogInformation($"PoliciesEvaluationResultEvent consumed! Unique room id: {policiesEvaluationResultEvent.RoomId}. " +
                                   $"Event message: {policiesEvaluationResultEvent.Message}");

            var getRoomCommand = new GetRoomCommand
            {
                RoomId = policiesEvaluationResultEvent.RoomId,
                NumberOfRecords = 1
            };
            var historicMeasurement =
                await _sensorStateHistoryHandler.GetHistoricMeasurements(getRoomCommand);
            policiesEvaluationResultEvent.MeasurementDate = historicMeasurement.Measurements.ElementAt(0).Item1.Date;
            //await _hubContext.Clients.Group(policiesEvaluationResultEvent.RoomId.ToString()).PoliciesEvaluationResultEvent(policiesEvaluationResultEvent.Message);
            await _hubContext.Clients.Group(policiesEvaluationResultEvent.RoomId.ToString()).PoliciesEvaluationResultEvent(policiesEvaluationResultEvent);
            _logger.LogInformation($"Message sent to admin web app.");
        }
    }
}