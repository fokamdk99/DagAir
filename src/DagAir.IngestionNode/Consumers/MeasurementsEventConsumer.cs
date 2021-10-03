using System;
using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;
using DagAir.IngestionNode.Infrastructure;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace DagAir.IngestionNode.Consumers
{
    public class MeasurementsEventConsumer : IConsumer<string>
    {
        private readonly ILogger<MeasurementsEventConsumer> _logger;

        public MeasurementsEventConsumer(ILogger<MeasurementsEventConsumer> logger)
        {
            _logger = logger;
        }
        
        public async Task Consume(ConsumeContext<string> context)
        {
            Console.WriteLine($"Temperature read: {context.Message}");
            _logger.LogInformation($"Temperature read: {context.Message}");
        }
    }
}