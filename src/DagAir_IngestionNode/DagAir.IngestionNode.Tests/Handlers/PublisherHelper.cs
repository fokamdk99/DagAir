using System;
using System.Threading.Tasks;
using FluentAssertions;
using MassTransit;
using MassTransit.Testing;
using NUnit.Framework;

namespace DagAir.IngestionNode.Tests.Handlers
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

            var messageHasBeenConsumed = await testHarness.Consumed.Any(x => x.Context.MessageId == messageIdentifier);
            messageHasBeenConsumed.Should().BeTrue();

            var message = await testHarness!.Consumed.SelectAsync(x => x.Context.MessageId == messageIdentifier).First();
            message.Exception.Should().BeNull("Message has been consumed without any errors");
        }
        internal static async Task CheckThatEventIsPublished<T>(InMemoryTestHarness testHarness) where T : class
        {
            var messageHasBennPublished = await testHarness.Published.Any<T>();
            Assert.IsTrue(messageHasBennPublished);
        }
    }
}