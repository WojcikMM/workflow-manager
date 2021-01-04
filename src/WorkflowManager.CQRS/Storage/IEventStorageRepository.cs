using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using WorkflowManager.CQRS.Domain.Events;
using WorkflowManager.CQRS.Domain.Domain.Mementos;

namespace WorkflowManager.CQRS.Storage
{
    public interface IEventStorageRepository
    {
        Task<bool> AnyCommittedEvents(Guid AggregateId);
        Task<IEnumerable<IEvent>> GetCommittedEventsAsync(Guid AggregateId);
        Task<IEnumerable<IEvent>> GetCommittedEventsAsync(Guid AggregateId, int lastMementoVersion);

        Task<BaseMemento> GetLatestMemento(Guid AggregateId);

        Task SaveSnapshotAsync(BaseMemento memento);

        Task AddEventAsync(IEvent @event);

    }
}
