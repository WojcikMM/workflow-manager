using System;
using System.Collections.Generic;

namespace WorkflowManagerMonolith.Infrastructure.EntityFramework.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<TransactionItemModel> TransactionItems { get; set; }
        public IEnumerable<ApplicationModel> AssignedApplications { get; set; }
    }
}

