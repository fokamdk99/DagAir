using System;
using System.Reflection;
using MassTransit.RabbitMqTransport;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;

namespace DagAir.MassTransit.RabbitMq
{
    public static class MassTransitExtensions
    {
        public static void ConfigureMassTransit(this IServiceCollectionBusConfigurator x, 
            Assembly assembly,
            Action<IRabbitMqBusFactoryConfigurator> configurator = null)
        {
            x.AddConsumers(assembly);
            
            x.SetKebabCaseEndpointNameFormatter();
            
            
        }
    }
}