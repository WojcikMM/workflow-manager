using WorkflowConfigurationService.Domain.Events;

namespace WorkflowConfigurationService.Domain.Bus
{
    public interface IEventBus
    {
        void Publish<T>(T @event) where T : BaseEvent;
    }
}
