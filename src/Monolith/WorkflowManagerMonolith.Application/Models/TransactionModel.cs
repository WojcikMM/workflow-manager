using System;

namespace WorkflowManagerMonolith.Application.Models
{
    public class TransactionModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid IncomingStatusId { get; set; }
        public Guid OutgoingStatusId { get; set; }

        public virtual StatusModel IncomingStatus { get; set; }
        public virtual StatusModel OutgoingStatus { get; set; }
    }
}

