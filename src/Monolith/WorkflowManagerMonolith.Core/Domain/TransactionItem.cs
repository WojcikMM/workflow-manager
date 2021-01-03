using System;
using WorkflowManagerMonolith.Core.Abstractions;

namespace WorkflowManagerMonolith.Core.Domain
{
    public class TransactionItem : IValueObject
    {
        public Guid Id { get; protected set; }
        public Guid ApplicationId { get; protected set; }
        public Guid UserId { get; protected set; }
        public Guid TransactionId { get; protected set; }
        public string TransactionName { get; protected set; }
        public string TransactionDescription { get; protected set; }
        public Guid OutgoingStatusId { get; protected set; }
        public string OutgoingStatusName { get; protected set; }
        public DateTime TransactionAt { get; protected set; }
        public Guid TransactionBy { get; protected set; }

        public virtual TransactionEntity Transaction { get; protected set; }
        public virtual UserEntity User { get; protected set; }
        public virtual ApplicationEntity Application { get; protected set; }

        protected TransactionItem() { }

        protected TransactionItem(TransactionEntity transaction, Guid userId)
        {
            TransactionId = transaction.Id;
            TransactionName = transaction.Name;
            TransactionDescription = transaction.Description;
            OutgoingStatusId = transaction.OutgoingStatusId;
            TransactionAt = DateTime.UtcNow;
            TransactionBy = userId;
        }

        public static TransactionItem Create(TransactionEntity transaction, Guid userId) =>
            new TransactionItem(transaction, userId);

    }

}
