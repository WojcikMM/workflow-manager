using System;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.Common.Messages.Events.Transactions
{
    public class TransactionCreatedEvent : BaseEvent
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid StatusId { get; set; }
        public Guid OutgoingStatusId { get; set; }

        public TransactionCreatedEvent(Guid AggregateId, string Name, string Description, Guid StatusId, Guid OutgoingStatusId) : base(AggregateId)
        {
            this.Name = Name;
            this.Description = Description;
            this.StatusId = StatusId;
            this.OutgoingStatusId = OutgoingStatusId;
        }
    }
}
