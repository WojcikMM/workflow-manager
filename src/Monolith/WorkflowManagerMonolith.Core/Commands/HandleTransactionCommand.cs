using System;
using WorkflowManagerMonolith.Core.Abstractions;

namespace WorkflowManagerMonolith.Core.Commands
{
    public class HandleTransactionCommand : ICommand
    {
        public Guid ApplicationId { get; set; }
        public Guid TransactionId { get; set; }
        public Guid UserId { get; set; }
    }
}
