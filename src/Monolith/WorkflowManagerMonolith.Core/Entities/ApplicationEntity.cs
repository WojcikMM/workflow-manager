using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkflowManagerMonolith.Core.Entities
{
    public class ApplicationEntity : Entity
    {
        public IEnumerable<TransactionEntity> TransactionsList { get; private set; }

        public StatusEntity? Status
        {
            get
            {
                return TransactionsList?.ToList()?.LastOrDefault()?.OutgoingStatus;
            }
        }


        public ApplicationEntity(Guid Id)
        {
            this.Id = Id;
            TransactionsList = new List<TransactionEntity>();
        }


        public void ApplyTransaction(TransactionEntity Transaction)
        {
            if(Status != null  && Transaction.IncomingStatus.Id != Status?.Id)
            {
                throw new Exception("Wrong transaction. Check configuration.");
            }
        }





    }

    public class TransactionEntity : Entity
    {
        public StatusEntity IncomingStatus { get; private set; }
        public StatusEntity OutgoingStatus { get; private set; }

    }
    public class StatusEntity : Entity
    {
        public IEnumerable<TransactionEntity> AvaliableTransactions { get; set; }
    }
}
