using System;
using System.Reflection;
using DagAir.AdminNode.Consumers;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.RabbitMqTransport;

namespace DagAir.AdminNode.Infrastructure
{
    public static class AdminNodeMassTransitExtensions
    {
        public static void ConfigureRabbitMqBus(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator cfg)
        {
            cfg.ConfigureEndpoints(context);
            cfg.ReceiveEndpoint(new TemporaryEndpointDefinition($"admin-node-{Guid.NewGuid()}"), e =>
            {
                e.ConfigureConsumer<PoliciesEvaluationResultEventConsumer>(context);
            });
        }

        public static void AddServices(IServiceCollectionBusConfigurator serviceCollectionBusConfigurator, Assembly assembly)
        {
            serviceCollectionBusConfigurator.AddConsumers(assembly);
            serviceCollectionBusConfigurator.AddConsumer<PoliciesEvaluationResultEventConsumer>();
        }
    }
}