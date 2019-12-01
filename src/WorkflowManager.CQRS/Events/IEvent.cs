using System;

namespace WorkflowManager.CQRS.Domain.Events
{
    public interface IEvent
    {
        Guid Id { get; }
        int Version { get; }
        Guid AggregateId { get; }

    }
}
