using System;
using WorkflowManager.Common.Messages.Events.Saga;

namespace WorkflowManager.Common.Messages.Events.Transactions.Rejected
{
    public class TransactionNotCreatedEvent : BaseRejectedEvent
    {
        public TransactionNotCreatedEvent(Guid AggregateId, string CauseText) : base(AggregateId, CauseText)
        {
        }
    }
}
