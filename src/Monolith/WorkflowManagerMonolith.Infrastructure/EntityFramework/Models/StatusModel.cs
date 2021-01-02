using System;
using System.Collections.Generic;

namespace WorkflowManagerMonolith.Infrastructure.EntityFramework.Models
{
    public class StatusModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public IEnumerable<TransactionModel> AvailableTransactions { get; set; }
        public IEnumerable<TransactionModel> IncomingTransactions { get; set; }
        public IEnumerable<ApplicationModel> ApplicationsWithStatus { get; set; }
    }
}

