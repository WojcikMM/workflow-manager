using System;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.Common.Messages.Events.Transactions
{
    public class TransactionNameUpdatedEvent : BaseEvent
    {
        public string Name { get; set; }
        public TransactionNameUpdatedEvent(Guid AggregateId, string Name) : base(AggregateId)
        {
            this.Name = Name;
        }
    }

}
