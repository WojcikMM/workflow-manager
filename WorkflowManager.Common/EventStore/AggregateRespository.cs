using CQRS.Template.Domain.Domain;
using CQRS.Template.Domain.Domain.Mementos;
using CQRS.Template.Domain.Events;
using CQRS.Template.Domain.Exceptions;
using CQRS.Template.Domain.Storage;
using NEventStore;
using RawRabbit.vNext.Disposable;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorkflowManager.ProcessService.Core;

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

            BaseMemento leatestSnapshot = _storage.Advanced.GetSnapshot(id, int.MaxValue)?.Payload as BaseMemento;

            using IEventStream stream = _storage.OpenStream(id);
            int leatestSnapshotVersion = leatestSnapshot is null ? -1 : leatestSnapshot.Version;
            System.Collections.Generic.List<BaseEvent> events = stream.CommittedEvents.Select(m => m.Body as BaseEvent).Where(m => m.Version > leatestSnapshotVersion).ToList();

            TAggregate obj = new TAggregate();
            if (leatestSnapshot != null)
            {
                ((IOriginator)obj).SetMemento(leatestSnapshot);
            }

            obj.LoadsFromHistory(events);
            return obj;
        }

        public async Task SaveAsync(TAggregate aggregate, int expectedVersion, Guid correlationId)
        {
            System.Collections.Generic.IEnumerable<BaseEvent> uncommitedEvents = aggregate.GetUncommittedChanges();
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

                int version = aggregate.Version;

                using (IEventStream stream = _storage.OpenStream(aggregate.AggregateId))
                {
                    foreach (BaseEvent @event in uncommitedEvents)
                    {
                        version++;
                        if (version > 2 && version % 3 == 0)
                        {
                            IOriginator originator = (IOriginator)aggregate;
                            BaseMemento memento = originator.GetMemento();

                            _storage.Advanced.AddSnapshot(new Snapshot(stream.StreamId, version, memento));
                        }
                        @event.Version = version;
                        stream.Add(new EventMessage() { Body = @event });
                    }
                    stream.CommitChanges(Guid.NewGuid());
                }

                foreach (BaseEvent @event in uncommitedEvents)
                {
                    dynamic desEvent = (dynamic)Convert.ChangeType(@event, @event.GetType());
                    await _busClient.PublishAsync(desEvent, correlationId);
                }
            }
        }
    }
}
