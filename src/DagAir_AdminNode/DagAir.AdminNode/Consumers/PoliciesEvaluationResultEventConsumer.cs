using System.Threading.Tasks;
using DagAir.AdminNode.Hubs;
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

        public PoliciesEvaluationResultEventConsumer()
        {
            
        }

        public PoliciesEvaluationResultEventConsumer(ILogger<PoliciesEvaluationResultEventConsumer> logger,
            IHubContext<ChatHub, IAdminNodeHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }
        public async Task Consume(ConsumeContext<PoliciesEvaluationResultEvent> context)
        {
            var policiesEvaluationResultEvent = context.Message;
            _logger.LogInformation($"PoliciesEvaluationResultEvent consumed! Unique room id: {policiesEvaluationResultEvent.RoomId}. " +
                                   $"Event message: {policiesEvaluationResultEvent.Message}");
            await _hubContext.Clients.Group(policiesEvaluationResultEvent.RoomId.ToString()).PoliciesEvaluationResultEvent(policiesEvaluationResultEvent.Message);
            _logger.LogInformation($"Message sent to admin web app.");
        }
    }
}