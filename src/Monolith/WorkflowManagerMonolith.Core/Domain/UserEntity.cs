using System;
using System.Collections.Generic;
using WorkflowManagerMonolith.Core.Abstractions;

namespace WorkflowManagerMonolith.Core.Domain
{
    public class UserEntity : Entity
    {
        public UserEntity(Guid Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
        public string Name { get; protected set; }

        public virtual IEnumerable<TransactionItem> TransactionItems { get; protected set; }
        public virtual IEnumerable<ApplicationEntity> AssignedApplications { get; protected set; }
    }
}