using System;
using WorkflowManager.CQRS.Domain.Commands;

namespace WorkflowManager.CQRS.Domain.Events
{
    public interface ICompleteEvent : IEvent
    {
        public ICompleteEvent Initialize(IEvent @event, Guid correlationId);
        public ICompleteEvent Initialize(ICommand command, Guid correlationId);
    }
}
