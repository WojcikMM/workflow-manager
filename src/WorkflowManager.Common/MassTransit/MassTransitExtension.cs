using System;
using System.Collections.Generic;
using System.Linq;
using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkflowManager.Common.ApplicationInitializer;
using WorkflowManager.Common.Configuration;

namespace WorkflowManager.Common.MassTransit
{
    public class ConsumerAssemblyTypeModel
    {
        public Type ConsumerType { get; set; }
        public Type MessageType { get; set; }
    }

    public static class MassTransitExtension
    {
        public static void AddMasstransitWithReflection(this IServiceCollection services, IDictionary<Type, Type> messageTypeAndClientTypeDictionary)
        {
            services.AddMassTransit(config =>
            {
                var consumerAssemblyTypes = messageTypeAndClientTypeDictionary.ToList().Select(dictVal => new ConsumerAssemblyTypeModel()
                {
                    ConsumerType = dictVal.Value,
                    MessageType = dictVal.Key
                }).ToList();

                consumerAssemblyTypes.ForEach(assembly =>
                {
                    config.AddConsumer(assembly.ConsumerType);
                });

                var isAzServiceBusConfig = services.GetValue<bool>("UseAzureServiceBus");

                if (isAzServiceBusConfig)
                {
                    //TODO: REFACTOR TO : RETURN "IBusControl" and add some action to define host (connectionString || user/pass/port)
                    MassTransitExtension.CreateConfigForAzureServiceBus(services, config, consumerAssemblyTypes);
                }
                else
                {
                    MassTransitExtension.CreateConfigForRabbitMq(services, config, consumerAssemblyTypes);
                }

                services.AddSingleton<IHostedService, MassTransitHostedService>();
            });

        }

        private static void CreateConfigForAzureServiceBus(IServiceCollection services, IServiceCollectionBusConfigurator config, List<ConsumerAssemblyTypeModel> consumerAssemblyTypes)
        {
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
                                        .Invoke(null, new object[] { consumerConfig, null });
                                    });
                                })
                    });
                });
            }));
        }
        // TODO: Provide configuration for handle RabbitMq Host/Username/Password
        private static void CreateConfigForRabbitMq(IServiceCollection services, IServiceCollectionBusConfigurator configurator, List<ConsumerAssemblyTypeModel> consumerAssemblyTypes)
        {

            var serviceConfigModel = services.GetOptions<ServiceConfigurationModel>("Service");

            configurator.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(busFactoryConfig =>
            {
                busFactoryConfig.Host("workflowmanager.rabbitmq", "/", h =>
                  {
                      h.Username("guest");
                      h.Password("guest");
                  });

                var serviceConfigModel = services.GetOptions<ServiceConfigurationModel>("Service");
                consumerAssemblyTypes.ForEach(assemblyType =>
             {
                 busFactoryConfig.ReceiveEndpoint($"{serviceConfigModel.Name}_{assemblyType.MessageType.Name}", cfg =>
                  {
                      cfg.Consumer(assemblyType.ConsumerType, (Type type) => provider.GetService(type));
                  });
             });
            }));
        }
    }
}
