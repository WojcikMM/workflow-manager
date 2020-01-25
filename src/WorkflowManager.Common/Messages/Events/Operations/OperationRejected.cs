using System;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.Common.Messages.Events.Operations
{
    public class OperationRejected : IEvent
    {
        public Guid Id { get; set; }

        public int Version { get; set; }

        public Guid AggregateId { get; set; }

        public string ExceptionMessage { get; set; }

        public string ExceptionStack { get; set; }

        public string BusinessResponse { get; set; }

        public Guid CorrelationId { get; set; }
    }
}
