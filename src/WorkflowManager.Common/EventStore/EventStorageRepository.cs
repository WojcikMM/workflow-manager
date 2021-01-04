using NEventStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkflowManager.CQRS.Domain.Domain.Mementos;
using WorkflowManager.CQRS.Domain.Events;
using WorkflowManager.CQRS.Storage;

namespace WorkflowManager.Common.EventStore
{
    public class EventStorageRepository : IEventStorageRepository
    {
        private readonly IStoreEvents storage;

        public EventStorageRepository(IStoreEvents storage)
        {
            this.storage = storage;
        }
        public Task AddEventAsync(IEvent @event)
        {
            //using IEventStream stream = _storage.OpenStream(Aggregate.AggregateId);
            // stream.Add(new EventMessage() { Body = @event });
            // stream.CommitChanges(Guid.NewGuid());
            return Task.CompletedTask;
        }

        public Task<bool> AnyCommittedEvents(Guid AggregateId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IEvent>> GetCommittedEventsAsync(Guid AggregateId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IEvent>> GetCommittedEventsAsync(Guid AggregateId, int lastMementoVersion)
        {
            throw new NotImplementedException();
        }

        public Task<BaseMemento> GetLatestMemento(Guid AggregateId)
        {
            throw new NotImplementedException();
        }

        public Task SaveSnapshotAsync(BaseMemento memento)
        {
            //_storage.Advanced.AddSnapshot(new Snapshot(stream.StreamId, version, memento));
            throw new NotImplementedException();
        }
    }
}
