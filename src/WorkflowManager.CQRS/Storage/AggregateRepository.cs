using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using WorkflowManager.CQRS.Domain;
using WorkflowManager.CQRS.Domain.Events;
using WorkflowManager.CQRS.Domain.Domain;
using WorkflowManager.CQRS.Storage.Mementos;
using WorkflowManager.CQRS.Domain.Exceptions;
using WorkflowManager.CQRS.Domain.Domain.Mementos;
using WorkflowManager.CQRS.Events;

namespace WorkflowManager.CQRS.Storage
{

    public class AggregateRepository<TAggregate> : IRepository<TAggregate> where TAggregate : AggregateRoot, IOriginator, new()
    {
        protected IEventPublisher eventPublisher;
        protected IEventStorageRepository eventStorageRepository;

        public AggregateRepository(IEventPublisher eventPublisher, IEventStorageRepository eventStorageRepository)
        {
            this.eventPublisher = eventPublisher;
            this.eventStorageRepository = eventStorageRepository;
        }

        public async Task<bool> AnyAsync(Guid id)
        {
            return await eventStorageRepository.AnyCommittedEvents(id);
        }

        public async Task<TAggregate> GetByIdAsync(Guid AggregateId)
        {

            BaseMemento leatestSnapshot = await eventStorageRepository.GetLatestMemento(AggregateId);
            int leatestSnapshotVersion = leatestSnapshot is null ? DomainConstants.NewAggregateVersion : leatestSnapshot.Version;

            IEnumerable<IEvent> events = await eventStorageRepository.GetCommittedEventsAsync(AggregateId, leatestSnapshotVersion);

            TAggregate obj = new TAggregate();
            if (leatestSnapshot != null)
            {
                obj.SetMemento(leatestSnapshot);
            }

            obj.LoadsFromHistory(events);
            return obj;
        }

        public async Task SaveAsync(TAggregate Aggregate, int ExpectedVersion, Guid CorrelationId)
        {
            IEnumerable<IEvent> uncommitedEvents = Aggregate.GetUncommittedChanges();
            if (uncommitedEvents.Any())
            {
                // TODO: Specyfi lock system in async
                // lock (_lockStorage)
                if (ExpectedVersion != DomainConstants.NewAggregateVersion)
                {
                    TAggregate item = await GetByIdAsync(Aggregate.AggregateId); // ???
                    if (item.Version != ExpectedVersion)
                    {
                        throw new AggregateConcurrencyException($"Aggregate {item.AggregateId} has been previously modified");
                    }
                }

                var version = Aggregate.Version;

                foreach (IEvent @event in uncommitedEvents)
                {
                    version++;
                    if (version > 2 && version % 3 == 0)
                    {

                        BaseMemento memento = Aggregate.GetMemento();
                        await eventStorageRepository.SaveSnapshotAsync(memento);

                    }
                    @event.Version = version;
                    @event.CorrelationId = CorrelationId;
                    await eventStorageRepository.AddEventAsync(@event);

                    await eventPublisher.PublishEvent(@event);
                }
            }
        }
    }
}
