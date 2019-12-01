using WorkflowManager.CQRS.Domain.Events;
using System;

namespace WorkflowManager.Common.Messages.Events.Processes
{
    public class ProcessRemovedEvent : BaseEvent
    {
        public ProcessRemovedEvent(Guid Id) : base(Id)
        {

        }
    }
}
