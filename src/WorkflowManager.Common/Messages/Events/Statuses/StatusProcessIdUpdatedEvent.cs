using System;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.Common.Messages.Events.Statuses
{
    public class StatusProcessIdUpdatedEvent : BaseEvent
    {
        public Guid ProcessId { get; }

        public StatusProcessIdUpdatedEvent(Guid AggregateId, Guid ProcessId) : base(AggregateId)
        {
            this.ProcessId = ProcessId;
        }
    }
}
