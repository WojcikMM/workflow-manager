using System;
using System.Threading.Tasks;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.CQRS.Domain.EventHandlers
{
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event, Guid correlationId);
    }
}
