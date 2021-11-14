using System.Reflection;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.RabbitMqTransport;

namespace DagAir.ClientNode.Infrastructure
{
    public class ClientNodeMassTransitExtensions
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