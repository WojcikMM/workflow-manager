using System;
using System.Collections.Generic;
using System.Linq;
using WorkflowManagerMonolith.Core.Abstractions;

namespace WorkflowManagerMonolith.Core.Domain
{

    public class ApplicationEntity : Entity
    {

        private ISet<TransactionItem> _transactionItems = new HashSet<TransactionItem>();

        public IEnumerable<TransactionItem> TransactionItems => _transactionItems;

        public Guid? StatusId
        {
            get
            {
                return _transactionItems?.ToList()?.LastOrDefault()?.OutgoingStatusId;
            }
        }
        public Guid? AssignedUserId { get; protected set; }


        public ApplicationEntity(Guid Id)
        {
            this.Id = Id;
        }
        public ApplicationEntity(Guid Id, Guid? AssignedUserId, IEnumerable<TransactionItem> transactionItems)
        {
            this.Id = Id;
            this.AssignedUserId = AssignedUserId;
            _transactionItems = transactionItems.ToHashSet();
        }


        public void ApplyTransaction(TransactionEntity transaction, Guid userId)
        {
            if (StatusId != null && transaction.IncomingStatusId != StatusId)
            {
                throw new Exception("Wrong transaction. Check configuration.");
            }

            if (userId == Guid.Empty)
            {
                throw new Exception("Wrong user id.");
            }

            _transactionItems.Add(TransactionItem.Create(transaction, userId));
            UpdatedAt = DateTime.UtcNow;
        }

        public void AssingnToHandling(Guid userId)
        {
            if (AssignedUserId != null && AssignedUserId != userId)
            {
                throw new Exception("Application assigned to another user. Need to release one first.");
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
