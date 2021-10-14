using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Testing;
using FluentAssertions;

namespace DagAir.IngestionNode.Tests.InfluxCommands
{
    internal static class PublisherHelper
    {
        internal static async Task<Guid> PublishMessage<T>(T @event, InMemoryTestHarness testHarness)
        {
            var guid = Guid.NewGuid();
            await testHarness.BusControl.Publish(@event,
                x => { x.MessageId = guid; });
            return guid;
        }

        internal static async Task PublishAndWaitToBeConsumed<T>(T @event, InMemoryTestHarness testHarness)
        {
            var messageIdentifier = await PublishMessage(@event, testHarness);

            var messageHasBeenConsumed = await testHarness.Published.Any(x => x.Context.MessageId == messageIdentifier);
            messageHasBeenConsumed.Should().BeTrue();

            var message = await testHarness!.Published.SelectAsync(x => x.Context.MessageId == messageIdentifier).First();
            message.Exception.Should().BeNull("Message has been consumed without any errors");
        }
    }
}