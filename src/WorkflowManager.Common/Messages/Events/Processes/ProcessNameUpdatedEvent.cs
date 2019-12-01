using WorkflowManager.CQRS.Domain.Events;
using System;

namespace WorkflowManager.Common.Messages.Events.Processes
{
    public class ProcessNameUpdatedEvent : BaseEvent
    {
        public string Name { get; private set; }

        public ProcessNameUpdatedEvent(Guid AggregateId, string Name) : base(AggregateId)
        {
            this.Name = Name;
        }

    }


}
