using System;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.Common.Messages.Events.Operations
{
    public class OperationCompleted : IEvent
    {
        public Guid Id { get; set; }

        public int Version { get; set; }

        public Guid AggregateId { get; set; }
    }
}
