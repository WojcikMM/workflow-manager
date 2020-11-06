using System;
using WorkflowManager.Common.Messages.Events.Saga;

namespace WorkflowManager.Common.Messages.Events.Statuses.Rejected
{
    public class StatusNotUpdatedBecauseWrongProcessId : BaseRejectedEvent
    {
        public StatusNotUpdatedBecauseWrongProcessId(Guid AggregateId) : base(AggregateId, "Cannot update Process ID in given Status. Given Process not exists.")
        {
        }
    }
}
