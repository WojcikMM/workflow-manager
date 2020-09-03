using System;

namespace WorkflowManager.CQRS.Domain.Events
{

    public abstract class BaseEvent : IEvent
    {
        public Guid Id { get; }
        public int Version { get; set; }
        public Guid AggregateId { get; set; }
        public Guid CorrelationId { get; set; }

        public BaseEvent(Guid AggregateId)
        {
            Id = Guid.NewGuid();
            this.AggregateId = AggregateId;
        }
    }
}
