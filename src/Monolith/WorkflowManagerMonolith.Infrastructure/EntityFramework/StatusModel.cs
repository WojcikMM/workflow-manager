using System;

namespace WorkflowManagerMonolith.Infrastructure.EntityFramework
{
    public class StatusModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid IncomingTransactionId { get; set; }
        public Guid OutgoingTransactionId { get; set; }

        public virtual TransactionModel IncomingTransaction { get; set; }
        public virtual TransactionModel OutgoingTransaction { get; set; }
    }
}

