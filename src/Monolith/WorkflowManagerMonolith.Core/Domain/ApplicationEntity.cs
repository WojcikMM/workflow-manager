using System;
using System.Collections.Generic;
using System.Linq;
using WorkflowManagerMonolith.Core.Abstractions;
using WorkflowManagerMonolith.Core.Exceptions;

namespace WorkflowManagerMonolith.Core.Domain
{

    public class ApplicationEntity : Entity
    {
        public Guid? StatusId
        {
            get
            {
                return TransactionItems?.ToList()?.LastOrDefault()?.OutgoingStatusId;
            }
        }
        public Guid? AssignedUserId { get; protected set; }

        public virtual UserEntity User { get; protected set; }
        public virtual IList<TransactionItem> TransactionItems { get; protected set; }


        public ApplicationEntity(Guid Id)
        {
            this.Id = Id;
            TransactionItems = new List<TransactionItem>();
        }
        public ApplicationEntity(Guid Id, Guid? AssignedUserId, IEnumerable<TransactionItem> transactionItems)
        {
            this.Id = Id;
            this.AssignedUserId = AssignedUserId;
            TransactionItems = transactionItems.ToList();
        }


        public void ApplyTransaction(TransactionEntity transaction, Guid userId)
        {
            if (StatusId != null && transaction.IncomingStatusId != StatusId)
            {
                throw new AggregateIllegalLogicException("Wrong transaction. Check configuration.");
            }

            if (userId == Guid.Empty)
            {
                throw new AggregateValidationException("Invalid user id.");
            }

            TransactionItems.Add(TransactionItem.Create(transaction, userId, Id));
            UpdatedAt = DateTime.UtcNow;
        }

        public void AssingnToHandling(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new AggregateValidationException("Invalid user id");
            }
            if (AssignedUserId != null && AssignedUserId != userId)
            {
                throw new AggregateIllegalLogicException("Application assigned to another user. Need to release one first.");
            }

            AssignedUserId = userId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ReleaseHandling()
        {
            AssignedUserId = null;
            UpdatedAt = DateTime.UtcNow;
        }

    }

}
