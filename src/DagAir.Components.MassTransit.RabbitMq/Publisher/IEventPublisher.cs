using System.Threading;
using System.Threading.Tasks;

namespace DagAir.Components.MassTransit.RabbitMq.Publisher
{
    public interface IEventPublisher
    {
        Task Publish<T>(T message, CancellationToken token = default) where T : class;
    }
}