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

namespace WorkflowManager.CQRS.Storage
{
    public abstract class BaseAggregateRepository<TAggregate> : IRepository<TAggregate> where TAggregate : AggregateRoot, IOriginator, new()
    {

        protected abstract IEnumerable<IEvent> GetEventsById(Guid AggregateId, int LeatestSnapshotVersion);
        protected abstract BaseMemento GetLatestSnapshot(Guid AggregateId);
        protected abstract Task SaveAndPublishEvents(TAggregate Aggregate, IEnumerable<IEvent> UncommitedEvents, Guid CorrelationId);

        public bool Any(Guid id)
        {
            return GetEventsById(id, DomainConstants.NewAggregateVersion).Any();
        }

        public TAggregate GetById(Guid AggregateId)
        {

            BaseMemento leatestSnapshot = GetLatestSnapshot(AggregateId);
            int leatestSnapshotVersion = leatestSnapshot is null ? DomainConstants.NewAggregateVersion : leatestSnapshot.Version;

            IEnumerable<IEvent> events = GetEventsById(AggregateId, leatestSnapshotVersion);

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
                    TAggregate item = GetById(Aggregate.AggregateId); // ???
                    if (item.Version != ExpectedVersion)
                    {
                        throw new AggregateConcurrencyException($"Aggregate {item.AggregateId} has been previously modified");
                    }
                }
                await SaveAndPublishEvents(Aggregate, uncommitedEvents, CorrelationId);
            }
        }
    }
}
