using System;
using CQRS.Template.Domain.Events;

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
