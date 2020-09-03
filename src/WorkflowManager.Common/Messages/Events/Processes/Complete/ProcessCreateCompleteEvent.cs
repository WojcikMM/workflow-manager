using System;
using WorkflowManager.Common.Messages.Events.Saga;

namespace WorkflowManager.Common.Messages.Events.Processes.Complete
{
    public class ProcessCreateCompleteEvent : BaseCompleteEvent
    {
        public ProcessCreateCompleteEvent(Guid AggregateId) : base(AggregateId, "Process created successfully.")
        {
        }
    }
}
