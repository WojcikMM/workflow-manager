using System;

namespace WorkflowManagerMonolith.Application.Models
{
    public class TransactionItemModel
    {
        public Guid TransactionId { get; set; }
        public Guid UserId { get; set; }
        public DateTime TransactionAt { get; set; }

        public virtual TransactionModel Transaction { get; set; }
        public virtual UserModel User { get; set; }
    }
}

