using System;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.Common.Messages.Events.Statuses
{
    public class StatusRemovedEvent : BaseEvent
    {
        public StatusRemovedEvent(Guid AggregateId) : base(AggregateId)
        {
        }
    }
}
