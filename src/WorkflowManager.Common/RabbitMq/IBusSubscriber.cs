using WorkflowManager.CQRS.Domain.Commands;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.Common.RabbitMq
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeCommand<TCommand>()
            where TCommand : ICommand;

        IBusSubscriber SubscribeCommand<TCommand, TRejectedEvent>()
            where TCommand : ICommand
            where TRejectedEvent : IRejectedEvent, new();

        IBusSubscriber SubscribeCommand<TCommand,TRejectedEvent, TCompleteEvent>()
            where TCommand : ICommand
            where TRejectedEvent : IRejectedEvent, new()
            where TCompleteEvent : ICompleteEvent, new();


        IBusSubscriber SubscribeEvent<TEvent>() 
            where TEvent : IEvent;


        IBusSubscriber SubscribeEvent<TEvent, TRejectedEvent>()
            where TEvent : IEvent
            where TRejectedEvent : IRejectedEvent, new();


        IBusSubscriber SubscribeEvent<TEvent, TRejectedEvent, TCompleteEvent>()
            where TEvent : IEvent
            where TRejectedEvent : IRejectedEvent, new()
            where TCompleteEvent : ICompleteEvent, new();
    }
}
