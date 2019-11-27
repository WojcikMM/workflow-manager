using CQRS.Template.Domain.Storage;
using Microsoft.Extensions.DependencyInjection;
using NEventStore;

namespace WorkflowManager.Common.EventStore
{
    public static class NEventStoreExtensions
    {
        public static void AddEventStore(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(typeof(IRepository<>), typeof(AggregateRespository<>));
            serviceCollection.AddTransient<IStoreEvents>(service => Wireup
                .Init()
                .LogToConsoleWindow(NEventStore.Logging.LogLevel.Info)
                .UsingInMemoryPersistence()
                .Build());

        }
    }
}
