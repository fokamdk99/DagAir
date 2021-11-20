using System.Threading.Tasks;
using DagAir.Components.MassTransit.RabbitMq.Publisher;
using DagAir.IngestionNode.Contracts;
using DagAir.PolicyNode.MeasurementCommands;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace DagAir.PolicyNode.Consumers
{
    public class MeasurementSentEventConsumer : IConsumer<MeasurementSentEvent>
    {
        private readonly IEvaluatePoliciesCommand _evaluatePoliciesCommand;
        private readonly IEventPublisher _eventPublisher;
        private readonly ILogger<MeasurementSentEventConsumer> _logger;

        public MeasurementSentEventConsumer(IEvaluatePoliciesCommand evaluatePoliciesCommand,
            IEventPublisher eventPublisher, ILogger<MeasurementSentEventConsumer> logger)
        {
            _evaluatePoliciesCommand = evaluatePoliciesCommand;
            _eventPublisher = eventPublisher;
            _logger = logger;
        }
        
        public async Task Consume(ConsumeContext<MeasurementSentEvent> context)
        {
            _logger.LogInformation($"Received MeasurementSentEvent. Temperature: {context.Message.Temperature}," +
                                   $"Illuminance: {context.Message.Illuminance}" +
                                   $"Humidity: {context.Message.Humidity}" +
                                   $"room id: {context.Message.RoomId}");
            var policiesEvaluationResultEvent = await _evaluatePoliciesCommand.Handle(context.Message);

            await _eventPublisher.Publish(policiesEvaluationResultEvent);
            _logger.LogInformation($"PoliciesEvaluationResultEvent sent from PolicyNode. Message: {policiesEvaluationResultEvent.Message}");
        }
    }
}