using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using NEventStore;
using NEventStore.Logging;
using NEventStore.Serialization.Json;
using WorkflowManager.Common.Configuration;
using WorkflowManager.CQRS.Storage;

namespace WorkflowManager.Common.EventStore
{
    public static class NEventStoreExtensions
    {
        public static void AddEventStore(this IServiceCollection serviceCollection, LogLevel logLevel = LogLevel.Info, string connectionStringName = "EventStoreDatabase")
        {
            var connectionString = serviceCollection.GetConnectionString(connectionStringName);

            serviceCollection.AddTransient(typeof(IRepository<>), typeof(AggregateRespository<>));

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
            serviceCollection.AddTransient(typeof(IRepository<>), typeof(AggregateRespository<>));
            serviceCollection.AddTransient(service => Wireup
                .Init()
                .LogToConsoleWindow(logLevel)
                .UsingInMemoryPersistence()
                .Build());
        }
    }
}
