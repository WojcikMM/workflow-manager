using System;
using WorkflowManager.CQRS.Domain.Commands;

namespace WorkflowManager.CQRS.Domain.Events
{
    public interface IRejectedEvent : IEvent
    {
        public string ExceptionMessage { get; }
        public string ExceptionStack { get; }
        public string BusinessResponse { get; }
        public IRejectedEvent Initialize(IEvent @event, Exception exception, Guid correlationId);
        public IRejectedEvent Initialize(ICommand command, Exception exception, Guid correlationId);
    }
}
