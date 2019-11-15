using System;
using CQRS.Template.Domain.Events;

namespace WorkflowConfigurationService.Core.Processes.Events
{
    public class ProcessRemovedEvent : BaseEvent
    {
        public ProcessRemovedEvent(Guid Id):base(Id)
        {

        }
    }
}
