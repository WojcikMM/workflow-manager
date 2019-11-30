﻿using CQRS.Template.Domain.Storage;
using Microsoft.Extensions.DependencyInjection;
using NEventStore;
using NEventStore.Logging;
using System.Data.SqlClient;
using NEventStore.Serialization.Json;
using Microsoft.Extensions.Configuration;
using System;

namespace WorkflowManager.Common.EventStore
{
    public static class NEventStoreExtensions
    {
        public static void AddEventStore(this IServiceCollection serviceCollection, LogLevel logLevel = LogLevel.Info, string connectionStringName = "EventStoreDatabase")
        {
            string connectionString = null;
            using (ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider())
            {
                IConfiguration configuration = serviceProvider.GetService<IConfiguration>();
                connectionString = configuration.GetConnectionString(connectionStringName);
            }
            if (connectionString is null)
            {
                throw new ApplicationException("Cannot find event store connectionString value.");
            }

            serviceCollection.AddSingleton(typeof(IRepository<>), typeof(AggregateRespository<>));

            serviceCollection.AddTransient<IStoreEvents>(service => Wireup
                .Init()
                .LogToConsoleWindow(logLevel)
                .UsingSqlPersistence(SqlClientFactory.Instance, connectionString)
                .WithDialect(new NEventStore.Persistence.Sql.SqlDialects.MsSqlDialect())
                .InitializeStorageEngine()
                .UsingJsonSerialization()
                .Build());

        }

        public static void AddInMemoryEventStore(this IServiceCollection serviceCollection, LogLevel logLevel = LogLevel.Info)
        {
            serviceCollection.AddSingleton(typeof(IRepository<>), typeof(AggregateRespository<>));
            serviceCollection.AddTransient<IStoreEvents>(service => Wireup
                .Init()
                .LogToConsoleWindow(logLevel)
                .UsingInMemoryPersistence()
                .Build());
        }
    }
}
