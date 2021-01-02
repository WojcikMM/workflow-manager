using System;

namespace WorkflowManagerMonolith.Infrastructure.EntityFramework.Models
{
    public class TransactionItemModel
    {
        public Guid Id { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid TransactionId { get; set; }
        public Guid UserId { get; set; }
        public DateTime TransactionAt { get; set; }

        public virtual TransactionModel Transaction { get; set; }
        public virtual UserModel User { get; set; }
        public virtual ApplicationModel Application { get; set; }
    }
}

