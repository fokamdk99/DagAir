using System.Reflection;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.RabbitMqTransport;

namespace DagAir.DataServices.SensorState.Infrastructure
{
    public class SensorStateMassTransitExtensions
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