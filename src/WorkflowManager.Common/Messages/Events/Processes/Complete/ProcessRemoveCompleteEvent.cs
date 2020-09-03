using System;
using WorkflowManager.Common.Messages.Events.Saga;

namespace WorkflowManager.Common.Messages.Events.Processes.Complete
{
    public class ProcessRemoveCompleteEvent : BaseCompleteEvent
    {
        public ProcessRemoveCompleteEvent(Guid AggregateId) : base(AggregateId, "Process removed successfully.")
        {
        }
    }
}
