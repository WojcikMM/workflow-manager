using System;
using WorkflowManager.Common.Messages.Events.Saga;

namespace WorkflowManager.Common.Messages.Events.Processes.Rejected
{
    public class ProcessCreateRejectedEvent : BaseRejectedEvent
    {
        public ProcessCreateRejectedEvent(Guid AggregateId) : base(AggregateId, "Could not create process with given id.")
        {
        }
    }
}
