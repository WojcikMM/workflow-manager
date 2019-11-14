namespace CQRS.Template.Domain.Events
{
    public interface IAggregateEventHandler<TEvent> where TEvent : BaseEvent
    {
        void HandleEvent(TEvent @event);
    }
}
