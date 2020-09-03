using System;

namespace WorkflowManager.CQRS.Domain.Commands
{
    public interface ICommand
    {
        Guid AggregateId { get; }
        int Version { get; }
        Guid CorrelationId { get; }
    }
}
