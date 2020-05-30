using System;
using System.Linq;
using System.Reflection;
using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkflowManager.Common.Configuration;

namespace WorkflowManager.Common.MassTransit
{
    public static class MassTransitExtension
    {
        public static void AddMasstransit(this IServiceCollection services)
        {
            services.AddMassTransit(config =>
            {

                var consumerAssemblyTypes = Assembly.GetEntryAssembly().GetTypes()
                .Where(t => t.IsClass && typeof(IConsumer).IsAssignableFrom(t))
                .Select(t => new
                {
                    ConsumerType = t,
                    MessageType = t.GetInterfaces().First().GetGenericArguments().First()
                })
                .ToList();

                consumerAssemblyTypes.ForEach(assembly =>
                {
                    config.AddConsumer(assembly.ConsumerType);
                });

                config.AddBus(provider => Bus.Factory.CreateUsingAzureServiceBus(busFactoryConfig =>
                    {
                        var connectionString = services.GetValue<string>("AzureServiceBusConnectionString");

                        busFactoryConfig.Host(connectionString);

                        consumerAssemblyTypes
                        .GroupBy(assembly => assembly.MessageType)
                        .Select(assemblyGroup => new
                        {
                            MessageType = assemblyGroup.Key,
                            ConsumerTypes = assemblyGroup.Select(assembly => assembly.ConsumerType).ToList()
                        })
                        .ToList()
                        .ForEach(assembly =>
                        {
                            busFactoryConfig.GetType()
                            .GetMethod(nameof(busFactoryConfig.SubscriptionEndpoint), 1, new Type[] {
                                typeof(string),
                                typeof(Action<IServiceBusSubscriptionEndpointConfigurator>)
                            })
                            .MakeGenericMethod(assembly.MessageType)
                            .Invoke(busFactoryConfig, new object[] {
                                assembly.MessageType.Name,
                                new Action<IServiceBusSubscriptionEndpointConfigurator>(consumerConfig =>
                                {
                                    var registerConsumerMethod = typeof(DependencyInjectionReceiveEndpointExtensions)
                                    .GetMethods()
                                    .Where(m=>m.IsGenericMethod && m.GetGenericArguments().Length == 1)
                                    .First();

                                    assembly.ConsumerTypes.ForEach(consumerType =>
                                    {
                                        registerConsumerMethod.MakeGenericMethod(consumerType)
                                        .Invoke(null, new object[] { consumerConfig, provider.Container, null });
                                    });
                                })
                            });
                        });
                    }));
            });

            services.AddSingleton<IHostedService, MassTransitHostedService>();
        }

    }
}
