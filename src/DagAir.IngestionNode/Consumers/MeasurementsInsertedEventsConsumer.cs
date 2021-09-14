using DagAir.IngestionNode.Contracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagAir.IngestionNode.Consumers
{
    public class MeasurementsInsertedEventsConsumer : IConsumer<IMeasurementsInsertedEvent>
    {
        public async Task Consume(ConsumeContext<IMeasurementsInsertedEvent> context)
        {
            Console.WriteLine($"Temperature read: {context.Message}");
        }
    }
}
