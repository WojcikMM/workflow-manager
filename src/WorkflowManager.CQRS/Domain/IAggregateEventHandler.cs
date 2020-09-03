namespace WorkflowManager.CQRS.Domain.Events
{
    public interface IAggregateEventHandler<TEvent> where TEvent : IEvent
    {
        void HandleEvent(TEvent @event);
    }
}
