using System;
using WorkflowManager.CQRS.Domain.Commands;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.Common.Messages.Events.Saga
{
    public class BaseRejectedEvent : BaseEvent, IRejectedEvent
    {
        public string ExceptionMessage { get; private set; }

        public string ExceptionStack { get; private set; }

        public string BusinessResponse { get; private set; }

        public BaseRejectedEvent(Guid AggregateId, string businessResponse): base(AggregateId)
        {
            this.BusinessResponse = businessResponse;
        }


        public IRejectedEvent Initialize(IEvent @event, Exception exception, Guid correlationId)
        {
            this.AggregateId = @event.AggregateId;
            this.CorrelationId = correlationId;
            this.Version = @event.Version;
            this.ExceptionMessage = exception.Message;
            this.ExceptionStack = exception.StackTrace;
            return this;
        }

        public IRejectedEvent Initialize(ICommand command, Exception exception, Guid correlationId)
        {
            this.AggregateId = command.AggregateId;
            this.CorrelationId = correlationId;
            this.Version = command.Version;
            this.ExceptionMessage = exception.Message;
            this.ExceptionStack = exception.StackTrace;
            return this;
        }
    }
}
