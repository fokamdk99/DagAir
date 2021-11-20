using System.Threading.Tasks;
using DagAir.ClientNode.Hubs;
using DagAir.PolicyNode.Contracts.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;

namespace DagAir.ClientNode.Consumers
{
    public class PoliciesEvaluationResultEventConsumer : IConsumer<PoliciesEvaluationResultEvent>
    {
        private readonly ILogger<PoliciesEvaluationResultEventConsumer> _logger;
        private readonly IHubContext<ChatHub, IClientNodeHub> _hubContext;

        public PoliciesEvaluationResultEventConsumer(ILogger<PoliciesEvaluationResultEventConsumer> logger,
            IHubContext<ChatHub, IClientNodeHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }
        public async Task Consume(ConsumeContext<PoliciesEvaluationResultEvent> context)
        {
            _logger.LogInformation($"PoliciesEvaluationResultEvent consumed! Event message: {context.Message.Message}");
            await _hubContext.Clients.Group("test_group").ReceiveMessage("client_node application", context.Message.Message);
            _logger.LogInformation($"Message sent to client web app.");
        }
    }
}