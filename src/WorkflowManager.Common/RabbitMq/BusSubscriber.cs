using WorkflowManager.CQRS.Domain.CommandHandlers;
using WorkflowManager.CQRS.Domain.Commands;
using WorkflowManager.CQRS.Domain.EventHandlers;
using WorkflowManager.CQRS.Domain.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit.vNext.Disposable;
using System;

namespace WorkflowManager.Common.RabbitMq
{


    public class BusSubscriber : IBusSubscriber
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IBusClient _busClient;

        public BusSubscriber(IApplicationBuilder app)
        {
            _serviceProvider = app.ApplicationServices.GetService<IServiceProvider>()
                ?? throw new ArgumentNullException(nameof(_serviceProvider), "Cannot get service provider.");
            _busClient = _serviceProvider.GetService<IBusClient>() ??
                throw new ArgumentException(nameof(_busClient), "Bus client was not registred in application.");
        }

        public IBusSubscriber SubscribeCommand<TCommand>(string @namespace = null, string queueName = null, Func<TCommand, Exception, IRejectedEvent> onError = null) where TCommand : ICommand
        {
            _busClient.SubscribeAsync<TCommand>(async (command, correlationContext) =>
            {
                ICommandHandler<TCommand> commandHandler = _serviceProvider.GetService<ICommandHandler<TCommand>>();

                try
                {
                    await commandHandler.HandleAsync(command, correlationContext.GlobalRequestId);
                }
                catch (Exception ex)
                {
                    if (!(onError is null))
                    {
                        IRejectedEvent rejectedEvent = onError(command, ex);
                        await _busClient.PublishAsync(rejectedEvent, correlationContext.GlobalRequestId);
                    }
                    // add logs
                }
            });

            return this;
        }

        public IBusSubscriber SubscribeEvent<TEvent>(string @namespace = null, string queueName = null, Func<TEvent, Exception, IRejectedEvent> onError = null) where TEvent : IEvent
        {
            _busClient.SubscribeAsync<TEvent>(async (@event, correlationContext) =>
            {
                System.Collections.Generic.IEnumerable<IEventHandler<TEvent>> eventHandlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();
                foreach (IEventHandler<TEvent> eventHandler in eventHandlers)
                {
                    try
                    {
                        await eventHandler.HandleAsync(@event);
                    }
                    catch (Exception ex)
                    {
                        if (!(onError is null))
                        {
                            IRejectedEvent rejectedEvent = onError(@event, ex);
                            await _busClient.PublishAsync(rejectedEvent, correlationContext.GlobalRequestId);
                        }
                        // add logs
                        break;
                    }
                }

            });

            return this;
        }
    }
}
