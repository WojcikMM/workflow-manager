using WorkflowManager.CQRS.Domain.CommandHandlers;
using WorkflowManager.CQRS.Domain.Commands;
using WorkflowManager.CQRS.Domain.EventHandlers;
using WorkflowManager.CQRS.Domain.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit.vNext.Disposable;
using System;
using Microsoft.Extensions.Logging;

namespace WorkflowManager.Common.RabbitMq
{
    public class BusSubscriber : IBusSubscriber
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IBusClient _busClient;
        private readonly ILogger<BusSubscriber> _logger;

        public BusSubscriber(IApplicationBuilder app)
        {
            _serviceProvider = app.ApplicationServices.GetService<IServiceProvider>()
                ?? throw new ArgumentNullException(nameof(_serviceProvider), "Cannot get service provider.");
            _busClient = _serviceProvider.GetService<IBusClient>() ??
                throw new ArgumentException(nameof(_busClient), "Bus client was not registred in application.");
            _logger = _serviceProvider.GetService<ILogger<BusSubscriber>>() ??
                throw new ArgumentException(nameof(_logger), "Logger not specyfied");
        }


        public IBusSubscriber SubscribeCommand<TCommand>()
            where TCommand : ICommand
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
                    LogErrorOnHandlers(ex, correlationContext.GlobalRequestId);
                }
            });

            return this;
        }

        public IBusSubscriber SubscribeCommand<TCommand, TRejectedEvent>()
         where TCommand : ICommand
         where TRejectedEvent : IRejectedEvent, new()
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
                    LogErrorOnHandlers(ex, correlationContext.GlobalRequestId);
                    var rejectedEvent = new TRejectedEvent()
                                        .Initialize(command, ex, correlationContext.GlobalRequestId);
                    await _busClient.PublishAsync(rejectedEvent, correlationContext.GlobalRequestId);
                }
            });

            return this;
        }

        public IBusSubscriber SubscribeCommand<TCommand, TRejectedEvent, TCompleteEvent>()
            where TCommand : ICommand
            where TRejectedEvent : IRejectedEvent, new()
            where TCompleteEvent : ICompleteEvent, new()
        {
            _busClient.SubscribeAsync<TCommand>(async (command, correlationContext) =>
            {
                ICommandHandler<TCommand> commandHandler = _serviceProvider.GetService<ICommandHandler<TCommand>>();

                try
                {
                    await commandHandler.HandleAsync(command, correlationContext.GlobalRequestId);

                    var completeEvent = new TCompleteEvent()
                                            .Initialize(command, correlationContext.GlobalRequestId);
                    await _busClient.PublishAsync(completeEvent, correlationContext.GlobalRequestId);
                }
                catch (Exception ex)
                {
                    LogErrorOnHandlers(ex, correlationContext.GlobalRequestId);
                    var rejectedEvent = new TRejectedEvent()
                                        .Initialize(command, ex, correlationContext.GlobalRequestId);
                    await _busClient.PublishAsync(rejectedEvent, correlationContext.GlobalRequestId);
                }
            });

            return this;
        }


        public IBusSubscriber SubscribeEvent<TEvent>()
           where TEvent : IEvent
        {
            _busClient.SubscribeAsync<TEvent>(async (@event, correlationContext) =>
            {
                System.Collections.Generic.IEnumerable<IEventHandler<TEvent>> eventHandlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();
                foreach (IEventHandler<TEvent> eventHandler in eventHandlers)
                {
                    try
                    {
                        await eventHandler.HandleAsync(@event, correlationContext.GlobalRequestId);
                    }
                    catch (Exception ex)
                    {
                        LogErrorOnHandlers(ex, correlationContext.GlobalRequestId);
                        break;
                    }
                }
            });

            return this;
        }


        public IBusSubscriber SubscribeEvent<TEvent, TRejectedEvent>()
           where TEvent : IEvent
           where TRejectedEvent : IRejectedEvent, new()
        {
            _busClient.SubscribeAsync<TEvent>(async (@event, correlationContext) =>
            {
                System.Collections.Generic.IEnumerable<IEventHandler<TEvent>> eventHandlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();
                foreach (IEventHandler<TEvent> eventHandler in eventHandlers)
                {
                    try
                    {
                        await eventHandler.HandleAsync(@event, correlationContext.GlobalRequestId);
                    }
                    catch (Exception ex)
                    {
                        LogErrorOnHandlers(ex, correlationContext.GlobalRequestId);
                        var rejectedEvent = new TRejectedEvent().Initialize(
                                                                @event, ex, correlationContext.GlobalRequestId);
                        await _busClient.PublishAsync(rejectedEvent, correlationContext.GlobalRequestId);
                        break;
                    }
                }

            });

            return this;
        }

        public IBusSubscriber SubscribeEvent<TEvent, TRejectedEvent, TCompleteEvent>()
            where TEvent : IEvent
            where TRejectedEvent : IRejectedEvent, new()
            where TCompleteEvent : ICompleteEvent, new()
        {
            _busClient.SubscribeAsync<TEvent>(async (@event, correlationContext) =>
            {
                System.Collections.Generic.IEnumerable<IEventHandler<TEvent>> eventHandlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();
                foreach (IEventHandler<TEvent> eventHandler in eventHandlers)
                {
                    try
                    {
                        await eventHandler.HandleAsync(@event, correlationContext.GlobalRequestId);

                        var completeEvent =new TCompleteEvent()
                                                .Initialize(@event, correlationContext.GlobalRequestId);

                        await _busClient.PublishAsync(completeEvent, correlationContext.GlobalRequestId);
                    }
                    catch (Exception ex)
                    {
                        LogErrorOnHandlers(ex, correlationContext.GlobalRequestId);

                        var rejectedEvent = new TRejectedEvent().Initialize(
                                                @event, ex, correlationContext.GlobalRequestId);
                        await _busClient.PublishAsync(rejectedEvent, correlationContext.GlobalRequestId);
                        break;
                    }
                }

            });

            return this;
        }


        private void LogErrorOnHandlers(Exception ex, Guid correlationId) => 
            _logger.LogError(ex, "Error occured when subscrbed bus.", new { CorrelationId = correlationId });
    }
}
