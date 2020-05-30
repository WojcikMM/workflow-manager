using System;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.Common.Messages.Events.Operations
{
    public class OperationPending : IEvent
    {
        public Guid Id { get; set; }

        public int Version { get; set; }

        public Guid AggregateId { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
