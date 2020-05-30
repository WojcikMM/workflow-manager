using System;
using WorkflowManager.Common.Messages.Events.Saga;

namespace WorkflowManager.Common.Messages.Events.Processes.Complete
{
    public class ProcessUpdateCompleteEvent : BaseCompleteEvent
    {
        public ProcessUpdateCompleteEvent(Guid AggregateId) : base(AggregateId, "Process updated successfully.")
        {
        }
    }
}
