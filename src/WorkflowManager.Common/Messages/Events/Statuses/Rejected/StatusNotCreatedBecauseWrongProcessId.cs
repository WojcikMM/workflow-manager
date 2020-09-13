using System;
using WorkflowManager.Common.Messages.Events.Saga;

namespace WorkflowManager.Common.Messages.Events.Statuses.Rejected
{
    public class StatusNotCreatedBecauseWrongProcessId : BaseRejectedEvent
    {
        public StatusNotCreatedBecauseWrongProcessId(Guid AggregateId) : base(AggregateId, "Cannot create Status. Given Process not exists.")
        {
        }
    }
}
