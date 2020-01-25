using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WorkflowManager.Common.Messages.Events.Operations;
using WorkflowManager.Common.RabbitMq;
using WorkflowManager.CQRS.Domain.Commands;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.OperationsStorage.Api
{
    public static class Subscriptions
    {
        private static readonly Assembly _messagesAssembly = typeof(Common.DomainConstants).Assembly;
        private static readonly ISet<Type> _excludedMessages = new HashSet<Type>(new[]
        {
            typeof(OperationPending),
            typeof(OperationCompleted),
            typeof(OperationRejected)
        });

        public static IBusSubscriber SubscribeAllMessages(this IBusSubscriber subscriber)
            => subscriber.SubscribeAllCommands().SubscribeAllEvents();

        private static IBusSubscriber SubscribeAllCommands(this IBusSubscriber subscriber)
            => subscriber.SubscribeAllMessages<ICommand>(nameof(IBusSubscriber.SubscribeCommand));

        private static IBusSubscriber SubscribeAllEvents(this IBusSubscriber subscriber)
            => subscriber.SubscribeAllMessages<IEvent>(nameof(IBusSubscriber.SubscribeEvent));



        private static IBusSubscriber SubscribeAllMessages<TMessage>
           (this IBusSubscriber subscriber, string subscribeMethod)
        {
            var messageTypes = _messagesAssembly
                .GetTypes()
                .Where(t => t.IsClass && typeof(TMessage).IsAssignableFrom(t))
                .Where(t => !_excludedMessages.Contains(t))
                .ToList();


            messageTypes.ForEach(mt => subscriber.GetType()
                .GetMethod(subscribeMethod,1, new Type[] { })
                .MakeGenericMethod(mt)
                .Invoke(subscriber, new object[] { }));

            return subscriber;
        }
    }
}
