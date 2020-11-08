using System;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.Common.Messages.Events.Transactions
{
    public class TransactionOutgoingStatusIdUpdatedEvent : BaseEvent
    {
        public Guid OutgoingStatusId { get; set; }
        public TransactionOutgoingStatusIdUpdatedEvent(Guid AggregateId, Guid OutgoingStatusId) : base(AggregateId)
        {
            this.OutgoingStatusId = OutgoingStatusId;
        }
    }

}
