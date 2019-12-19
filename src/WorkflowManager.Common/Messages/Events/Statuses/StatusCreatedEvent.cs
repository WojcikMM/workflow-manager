using System;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.Common.Messages.Events.Statuses
{
    public class StatusCreatedEvent : BaseEvent
    {
        public string Name { get; }
        public Guid ProcessId { get; }

        public StatusCreatedEvent(Guid AggregateId, string Name, Guid ProcessId) : base(AggregateId)
        {
            this.Name = Name;
            this.ProcessId = ProcessId;
        }
    }
}
