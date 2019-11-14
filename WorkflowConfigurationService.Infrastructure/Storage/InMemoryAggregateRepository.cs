using CQRS.Template.Domain.Domain;
using CQRS.Template.Domain.Domain.Mementos;
using CQRS.Template.Domain.Events;
using CQRS.Template.Domain.Exceptions;
using CQRS.Template.Domain.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkflowConfigurationService.Infrastructure.Storage
{
    public class InMemoryAggregateRepository<TAggregate> : IRepository<TAggregate> where TAggregate : AggregateRoot, new()
    {
        private readonly IEventStorage _storage;
        private static object _lockStorage = new object();

        public InMemoryAggregateRepository(IEventStorage storage)
        {
            _storage = storage;
        }

        public TAggregate GetById(Guid id)
        {
            IEnumerable<BaseEvent> events;
            var memento = _storage.GetMemento<BaseMemento>(id);
            if (memento != null)
            {
                events = _storage.GetEvents(id).Where(e => e.Version >= memento.Version);
            }
            else
            {
                events = _storage.GetEvents(id);
            }
            var obj = new TAggregate();
            if (memento != null)
                ((IOriginator)obj).SetMemento(memento);

            obj.LoadsFromHistory(events);
            return obj;
        }

        public void Save(TAggregate aggregate, int expectedVersion)
        {
            if (aggregate.GetUncommittedChanges().Any())
            {
                lock (_lockStorage)
                {
                    var item = new TAggregate();

                    if (expectedVersion != DomainConstants.NewAggregateVersion)
                    {
                        item = GetById(aggregate.AggregateId);
                        if (item.Version != expectedVersion)
                        {
                            throw new ConcurrencyException($"Aggregate {item.AggregateId} has been previously modified");
                        }
                    }

                    _storage.Save(aggregate);
                }
            }
        }
    }
}
