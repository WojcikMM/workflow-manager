using Microsoft.Extensions.DependencyInjection;
using NEventStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowManager.Common.EventStore
{
    public static class NEventStoreExtensions
    {
        public static void AddEventStore(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IStoreEvents>(service => Wireup
                .Init()
                .LogToConsoleWindow(NEventStore.Logging.LogLevel.Info)
                .UsingInMemoryPersistence()
                .Build());

        }
    }
}
