using System;
using CQRS.Template.Domain.Events;

namespace WorkflowManager.Common.Messages.Events.Processes
{
    public class ProcessRemovedEvent : BaseEvent
    {
        public ProcessRemovedEvent(Guid Id):base(Id)
        {

        }
    }
}
