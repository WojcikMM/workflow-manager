using System;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.Common.Messages.Events.Transactions
{
    public class TransactionDescriptionUpdatedEvent : BaseEvent
    {
        public string Description { get; set; }
        public TransactionDescriptionUpdatedEvent(Guid AggregateId, string Description) : base(AggregateId)
        {
            this.Description = Description;
        }
    }

}
