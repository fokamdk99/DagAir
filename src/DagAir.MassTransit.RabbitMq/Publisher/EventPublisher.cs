using System.Threading;
using System.Threading.Tasks;
using MassTransit;

namespace DagAir.MassTransit.RabbitMq.Publisher
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventPublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task Publish<T>(T message, CancellationToken token = default) where T : class
        {
            return _publishEndpoint.Publish(message, token);
        }
    }
}