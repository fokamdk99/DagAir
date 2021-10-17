using System;
using DagAir.Components.MassTransit.RabbitMq.Configuration;
using MassTransit.RabbitMqTransport;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;

namespace DagAir.MassTransit.RabbitMq
{
    public static class MassTransitExtensions
    {
        public static void ConfigureMassTransit<TRabbitMqConfiguration>(
            this IServiceCollectionBusConfigurator x, 
            Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? rabbitMqBusConfigurator = null,
            Action<IServiceCollectionBusConfigurator>? serviceCollectionBusConfigurator = null
            ) 
            where TRabbitMqConfiguration : class, IRabbitMqConfiguration
        {
            x.ApplyConfiguration<TRabbitMqConfiguration>(rabbitMqBusConfigurator);
            if (serviceCollectionBusConfigurator != null)
            {
                serviceCollectionBusConfigurator(x);
            }
            
        }

        private static void ApplyConfiguration<TRabbitMqConfiguration>(
            this IServiceCollectionBusConfigurator x,
            Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? rabbitMqBusConfigurator = null
            )
            where TRabbitMqConfiguration : class, IRabbitMqConfiguration
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.UsingRabbitMq((context, cfg) =>
            {
                if (rabbitMqBusConfigurator != null)
                {
                    ApplyConfiguration<TRabbitMqConfiguration>(context, cfg, rabbitMqBusConfigurator);
                }
                else
                {
                    ApplyConfiguration<TRabbitMqConfiguration>(context, cfg);
                }
            });
        }

        private static void ApplyConfiguration<T>(
            IBusRegistrationContext context,
            IRabbitMqBusFactoryConfigurator cfg,
            Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? rabbitMqBusConfigurator = null
        )
            where T : class, IRabbitMqConfiguration
        {
            var configuration = context.GetRequiredService<T>();
            cfg.Host(configuration.HostName,  configuration.VirtualHost, rcfg =>
            {
                rcfg.Username(configuration.UserName);
                rcfg.Password(configuration.Password);
            });

            if (rabbitMqBusConfigurator != null)
            {
                rabbitMqBusConfigurator(context, cfg);
            }
        }
    }
}