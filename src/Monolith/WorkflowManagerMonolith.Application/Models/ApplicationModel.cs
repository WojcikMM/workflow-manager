using System;
using System.Collections.Generic;

namespace WorkflowManagerMonolith.Application.Models
{
    public class ApplicationModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid? StatusId { get; set; }
        public Guid? UserId { get; set; }

        public virtual UserModel User { get; set; }
        public virtual StatusModel Status { get; set; }
        public virtual IEnumerable<TransactionItemModel> TransactionItems { get; set; }
    }
}

