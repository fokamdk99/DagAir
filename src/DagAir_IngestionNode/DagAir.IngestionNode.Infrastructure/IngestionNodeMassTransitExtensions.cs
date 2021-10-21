using System.Reflection;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.RabbitMqTransport;

namespace DagAir.IngestionNode.Infrastructure
{
    public class IngestionNodeMassTransitExtensions
    {
        public static void ConfigureRabbitMqBus(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator cfg)
        {
            cfg.ConfigureEndpoints(context);
        }
        
        public static void AddServices(IServiceCollectionBusConfigurator serviceCollectionBusConfigurator, Assembly assembly)
        {
            serviceCollectionBusConfigurator.AddConsumers(assembly);
        }
    }
}