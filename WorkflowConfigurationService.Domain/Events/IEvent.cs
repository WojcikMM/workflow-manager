using System;

namespace WorkflowConfigurationService.Domain.Events
{
    public interface IEvent
    {
        public Guid Id { get; }
        public int Version { get; }
        public Guid AggregateId { get; }
    }
}