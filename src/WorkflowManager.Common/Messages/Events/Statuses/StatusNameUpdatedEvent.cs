using System;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.Common.Messages.Events.Statuses
{
    public class StatusNameUpdatedEvent : BaseEvent
    {
        public string Name { get; }

        public StatusNameUpdatedEvent(Guid AggregateId, string Name) : base(AggregateId)
        {
            this.Name = Name;
        }
    }
}
