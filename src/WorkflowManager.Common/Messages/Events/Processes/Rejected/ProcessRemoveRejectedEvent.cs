using System;
using WorkflowManager.Common.Messages.Events.Saga;

namespace WorkflowManager.Common.Messages.Events.Processes.Rejected
{
    public class ProcessRemoveRejectedEvent : BaseRejectedEvent
    {
        public ProcessRemoveRejectedEvent(Guid AggregateId) : base(AggregateId, "Could not remove process with given id.")
        {
        }
    }

}
