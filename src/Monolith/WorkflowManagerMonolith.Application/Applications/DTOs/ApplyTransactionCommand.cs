using System;

namespace WorkflowManagerMonolith.Application.Applications.DTOs
{
    public class ApplyTransactionCommand
    {
        public Guid ApplicationId { get; set; }
        public Guid UserId { get; set; }
        public Guid TransactionId { get; set; }
    }
}
