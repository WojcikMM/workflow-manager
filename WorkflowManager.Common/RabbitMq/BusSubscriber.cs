using System;
using Microsoft.AspNetCore.Builder;
using CQRS.Template.Domain.Events;
using CQRS.Template.Domain.Commands;
using CQRS.Template.Domain.EventHandlers;
using CQRS.Template.Domain.CommandHandlers;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit.vNext.Disposable;

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
                var commandHandler = _serviceProvider.GetService<ICommandHandler<TCommand>>();

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
                try
                {

                    var eventHandlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();
                    foreach (var eventHandler in eventHandlers)
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
                }
                catch (Exception ex)
                {
                    var s = "";
                }
            });

            return this;
        }
    }
}
