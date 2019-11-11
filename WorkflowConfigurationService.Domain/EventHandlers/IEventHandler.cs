using WorkflowConfigurationService.Domain.Events;

namespace WorkflowConfigurationService.Domain.EventHandlers
{
    public interface IEventHandler<TEvent> where TEvent : BaseEvent
    {
        void Handle(TEvent handle);
    }
}
