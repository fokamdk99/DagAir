﻿using System;
using System.Reflection;
using DagAir.MassTransit.RabbitMq.Configuration;
using DagAir.MassTransit.RabbitMq.Publisher;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.MassTransit.RabbitMq
{
    public static class MassTransitFeature
    {
        public static IServiceCollection AddMassTransitFeature<TRabbitMqConfiguration>(
            this IServiceCollection services, 
            IConfiguration configuration, 
            Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? rabbitMqBusConfigurator = null,
            Action<IServiceCollectionBusConfigurator>? serviceCollectionBusConfigurator = null
            )
            where TRabbitMqConfiguration : class, IRabbitMqConfiguration
        {
            services.AddMassTransit(x =>
            {
                x.ConfigureMassTransit<TRabbitMqConfiguration>(rabbitMqBusConfigurator, serviceCollectionBusConfigurator);
            });

            services.AddMassTransitHostedService();

            return services;
        }
    }
}