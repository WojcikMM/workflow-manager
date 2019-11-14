using CQRS.Template.Domain.Events;

namespace CQRS.Template.Domain.EventHandlers
{
    public interface IEventHandler<TEvent> where TEvent : BaseEvent
    {
        void Handle(TEvent handle);
    }
}
