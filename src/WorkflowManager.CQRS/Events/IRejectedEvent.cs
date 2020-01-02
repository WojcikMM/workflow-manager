using System;
using WorkflowManager.CQRS.Domain.Commands;

namespace WorkflowManager.CQRS.Domain.Events
{
    public interface ISagaStatusEvent : IEvent
    {
        public Guid CorrelationId { get; }
    }


    public interface IRejectedEvent : ISagaStatusEvent
    {
        public string ExceptionMessage { get; }
        public string ExceptionStack { get; }
        public string BusinessResponse { get; }
        public IRejectedEvent Initialize(IEvent @event, Exception exception, Guid correlationId);
        public IRejectedEvent Initialize(ICommand command, Exception exception, Guid correlationId);
    }

    public interface ICompleteEvent : ISagaStatusEvent
    {
        public ICompleteEvent Initialize(IEvent @event, Guid correlationId);
        public ICompleteEvent Initialize(ICommand command, Guid correlationId);
    }
}
