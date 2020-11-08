using System;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.Common.Messages.Events.Transactions
{
    public class TransactionStatusIdUpdatedEvent : BaseEvent
    {
        public Guid StatusId { get; set; }
        public TransactionStatusIdUpdatedEvent(Guid AggregateId, Guid StatusId) : base(AggregateId)
        {
            this.StatusId = StatusId;
        }
    }

}
