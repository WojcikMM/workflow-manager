using System;
using CQRS.Template.Domain.Events;

namespace WorkflowConfigurationService.Core.Processes.Events
{
    public class ProcessCreatedEvent : BaseEvent
    {
        public string Name { get; private set; }

        public ProcessCreatedEvent(Guid Id, string Name) : base(Id)
        {
            this.Name = Name;
        }

    }
}
