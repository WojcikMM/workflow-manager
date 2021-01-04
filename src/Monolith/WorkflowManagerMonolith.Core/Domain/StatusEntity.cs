using System;
using System.Collections.Generic;
using WorkflowManagerMonolith.Core.Abstractions;

namespace WorkflowManagerMonolith.Core.Domain
{
    public class StatusEntity : Entity
    {
        public StatusEntity(Guid Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public string Name { get; protected set; }

        public virtual IEnumerable<TransactionEntity> AvailableTransactions { get; protected set; }
        public virtual IEnumerable<TransactionEntity> IncomingTransactions { get; protected set; }
        public virtual IEnumerable<ApplicationEntity> ApplicationsWithStatus { get; protected set; }

    }
}
