using System;
using WorkflowManager.Common.Messages.Events.Saga;

namespace WorkflowManager.Common.Messages.Events.Processes.Rejected
{
    public class ProcessNameUpdateRejectedEvent : BaseRejectedEvent
    {
        public ProcessNameUpdateRejectedEvent(Guid AggregateId) : base(AggregateId, "Could not update process name with given id.")
        {
        }
    }

}
