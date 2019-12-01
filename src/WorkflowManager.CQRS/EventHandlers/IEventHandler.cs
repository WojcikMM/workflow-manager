using WorkflowManager.CQRS.Domain.Events;
using System.Threading.Tasks;

namespace WorkflowManager.CQRS.Domain.EventHandlers
{
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
