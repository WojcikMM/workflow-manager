using System;
using CQRS.Template.Domain.Events;

namespace WorkflowConfigurationService.Core.Processes.Events
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
