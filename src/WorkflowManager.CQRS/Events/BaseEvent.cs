using System;

namespace WorkflowManager.CQRS.Domain.Events
{

    public abstract class BaseEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public Guid AggregateId { get; set; }

        protected BaseEvent() { }
        public BaseEvent(Guid AggregateId)
        {
            Id = Guid.NewGuid();
            this.AggregateId = AggregateId;
        }
    }
}
