using System;
using System.Collections.Generic;

namespace WorkflowManagerMonolith.Infrastructure.EntityFramework.Models
{
    public class TransactionModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid IncomingStatusId { get; set; }
        public Guid OutgoingStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual StatusModel IncomingStatus { get; set; }
        public virtual StatusModel OutgoingStatus { get; set; }
        public virtual IEnumerable<TransactionItemModel> TransactionItems { get; set; }
    }
}

