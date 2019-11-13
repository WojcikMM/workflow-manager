using System;

namespace WorkflowConfigurationService.Domain.Events.Processes
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
