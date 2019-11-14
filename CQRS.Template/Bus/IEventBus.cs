using CQRS.Template.Domain.Events;

namespace CQRS.Template.Domain.Bus
{
    public interface IEventBus
    {
        void Publish<T>(T @event) where T : BaseEvent;
    }
}
