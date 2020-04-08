using WorkflowManager.CQRS.Domain.Storage;
using Microsoft.Extensions.DependencyInjection;
using NEventStore;
using NEventStore.Logging;
using System.Data.SqlClient;
using NEventStore.Serialization.Json;
using WorkflowManager.Common.Configuration;

namespace WorkflowManager.Common.EventStore
{
    public static class NEventStoreExtensions
    {
        public static void AddEventStore(this IServiceCollection serviceCollection, LogLevel logLevel = LogLevel.Info, string connectionStringName = "EventStoreDatabase")
        {
            var connectionString = serviceCollection.GetConnectionString(connectionStringName);

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
