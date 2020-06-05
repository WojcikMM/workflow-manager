using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using NEventStore;
using WorkflowManager.CQRS.Domain.Domain;
using WorkflowManager.CQRS.Domain.Domain.Mementos;
using WorkflowManager.CQRS.Domain.Events;
using WorkflowManager.CQRS.Storage;
using WorkflowManager.CQRS.Storage.Mementos;

namespace WorkflowManager.Common.EventStore
{
    public class AggregateRespository<TAggregate> : BaseAggregateRepository<TAggregate>, IRepository<TAggregate> where TAggregate : AggregateRoot, IOriginator, new()
    {
        private readonly IStoreEvents _storage;
        private readonly IPublishEndpoint _publishEndpoint;

        public AggregateRespository(IStoreEvents storage, IPublishEndpoint busClient)
        {
            _storage = storage;
            _publishEndpoint = busClient;
        }

        protected override IEnumerable<IEvent> GetEventsById(Guid AggregateId, int LeatestSnapshotVersion)
        {
            using IEventStream stream = _storage.OpenStream(AggregateId);
            return stream.CommittedEvents.Select(m => m.Body as IEvent).Where(m => m.Version > LeatestSnapshotVersion).ToList();

        }

        protected override BaseMemento GetLatestSnapshot(Guid AggregateId)
        {
            return _storage.Advanced.GetSnapshot(AggregateId, int.MaxValue)?.Payload as BaseMemento;
        }

        protected override Task SaveAndPublishEvents(TAggregate Aggregate, IEnumerable<IEvent> UncommitedEvents, Guid CorrelationId)
        {
            var version = Aggregate.Version;

            using IEventStream stream = _storage.OpenStream(Aggregate.AggregateId);
            foreach (IEvent @event in UncommitedEvents)
            {
                version++;
                if (version > 2 && version % 3 == 0)
                {

                    BaseMemento memento = Aggregate.GetMemento();

                    _storage.Advanced.AddSnapshot(new Snapshot(stream.StreamId, version, memento));
                }
                @event.Version = version;
                @event.CorrelationId = CorrelationId;
                stream.Add(new EventMessage() { Body = @event });
                _publishEndpoint.Publish(@event, @event.GetType());
            }
            stream.CommitChanges(Guid.NewGuid());

            return Task.CompletedTask;
        }
    }
}
