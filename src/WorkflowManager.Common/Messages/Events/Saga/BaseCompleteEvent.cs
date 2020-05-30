using System;
using WorkflowManager.CQRS.Domain.Commands;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.Common.Messages.Events.Saga
{
    public class BaseCompleteEvent : BaseEvent, ICompleteEvent
    {
        public string BusinessResponse { get; }

        public BaseCompleteEvent(Guid AggregateId, string BusinessResponse) : base(AggregateId)
        {
            this.BusinessResponse = BusinessResponse;
        }

        public ICompleteEvent Initialize(IEvent @event, Guid correlationId)
        {

            this.CorrelationId = correlationId;
            this.Version = @event.Version;
            this.AggregateId = @event.AggregateId;
            return this;
        }

        public ICompleteEvent Initialize(ICommand command, Guid correlationId)
        {
            this.CorrelationId = correlationId;
            this.Version = command.Version;
            this.AggregateId = command.Id;
            return this;

        }
    }
}
