using System;

namespace WorkflowManagerMonolith.Application.Transactions.DTOs
{
    public class UpdateTransactionCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? IncomingStatusId { get; set; }
        public Guid? OutgoingStatusId { get; set; }
    }
}
