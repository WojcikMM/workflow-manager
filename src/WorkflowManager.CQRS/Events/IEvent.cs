using System;

namespace WorkflowManager.CQRS.Domain.Events
{
    public interface IEvent
    {
        Guid Id { get; }
        int Version { get; set; }
        Guid AggregateId { get; }
        Guid CorrelationId { get; set; }
    }
}
