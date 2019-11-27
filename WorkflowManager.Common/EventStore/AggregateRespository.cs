using System;
using NEventStore;
using System.Linq;
using System.Threading.Tasks;
using RawRabbit.vNext.Disposable;
using CQRS.Template.Domain.Domain;
using CQRS.Template.Domain.Storage;
using CQRS.Template.Domain.Exceptions;
using WorkflowManager.ProcessService.Core;
using CQRS.Template.Domain.Domain.Mementos;
using CQRS.Template.Domain.Events;

namespace WorkflowManager.Common.EventStore
{
    public class AggregateRespository<TAggregate> : IRepository<TAggregate> where TAggregate : AggregateRoot, new()
    {
        private readonly IStoreEvents _storage;
        private readonly IBusClient _busClient;

        public AggregateRespository(IStoreEvents storage, IBusClient busClient)
        {
            _storage = storage;
            _busClient = busClient;
        }

        public TAggregate GetById(Guid id)
        {

            var leatestSnapshot = _storage.Advanced.GetSnapshot(id, int.MaxValue)?.Payload as BaseMemento;

            using var stream = _storage.OpenStream(id);
            var leatestSnapshotVersion = leatestSnapshot is null ? -1 : leatestSnapshot.Version;
            var events = stream.CommittedEvents.Select(m => m.Body as BaseEvent).Where(m => m.Version > leatestSnapshotVersion).ToList();

            var obj = new TAggregate();
            if (leatestSnapshot != null)
            {
                ((IOriginator)obj).SetMemento(leatestSnapshot);
            }

            obj.LoadsFromHistory(events);
            return obj;
        }

        public async Task SaveAsync(TAggregate aggregate, int expectedVersion, Guid correlationId)
        {
            var uncommitedEvents = aggregate.GetUncommittedChanges();
            if (uncommitedEvents.Any())
            {
                // TODO: Specyfi lock system in async
                // lock (_lockStorage)
                if (expectedVersion != DomainConstants.NewAggregateVersion)
                {
                    TAggregate item = GetById(aggregate.AggregateId); // ???
                    if (item.Version != expectedVersion)
                    {
                        throw new AggregateConcurrencyException($"Aggregate {item.AggregateId} has been previously modified");
                    }
                }

                var version = aggregate.Version;

                using (var stream = _storage.OpenStream(aggregate.AggregateId))
                {
                    foreach (var @event in uncommitedEvents)
                    {
                        version++;
                        if (version > 2 && version % 3 == 0)
                        {
                            var originator = (IOriginator)aggregate;
                            var memento = originator.GetMemento();

                            _storage.Advanced.AddSnapshot(new Snapshot(stream.StreamId, version, memento));
                        }
                        @event.Version = version;
                        stream.Add(new EventMessage() { Body = @event });
                    }
                    stream.CommitChanges(Guid.NewGuid());
                }

                foreach (var @event in uncommitedEvents)
                {
                    var desEvent = (dynamic)Convert.ChangeType(@event, @event.GetType());
                    await _busClient.PublishAsync(desEvent, correlationId);
                }
            }
        }
    }
}
