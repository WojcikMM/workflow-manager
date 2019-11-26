using System;

namespace CQRS.Template.Domain.Events
{
    public interface IEvent
    {
        Guid Id { get; }
        int Version { get; }
        Guid AggregateId { get; }

    }
}
