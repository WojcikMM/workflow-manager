using CQRS.Template.Domain.Events;
using System.Threading.Tasks;

namespace CQRS.Template.Domain.EventHandlers
{
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
