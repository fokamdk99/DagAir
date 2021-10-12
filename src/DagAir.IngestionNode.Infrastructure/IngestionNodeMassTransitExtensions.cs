using System.Threading.Tasks;
using MassTransit;
using MassTransit.RabbitMqTransport;

namespace DagAir.IngestionNode.Infrastructure
{
    public class IngestionNodeMassTransitExtensions
    {
        public static void ConfigureRabbitMqBus(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator cfg)
        {
            cfg.ConfigureEndpoints(context);
        }
    }
}