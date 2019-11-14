using System;
using System.Linq;
using CQRS.Template.Domain.Bus;
using System.Collections.Generic;
using CQRS.Template.Domain.Domain;
using CQRS.Template.Domain.Events;
using CQRS.Template.Domain.Storage;
using CQRS.Template.Domain.Exceptions;
using CQRS.Template.Domain.Domain.Mementos;
using System.Threading.Tasks;

namespace WorkflowConfigurationService.Infrastructure.Storage
{
    public class InMemoryEventStorage : IEventStorage
    {
        private readonly List<BaseEvent> _events;
        private readonly List<BaseMemento> _mementos;
        private readonly IEventBus _eventBus;

        public InMemoryEventStorage(IEventBus eventBus)
        {
            _events = new List<BaseEvent>();
            _mementos = new List<BaseMemento>();
            _eventBus = eventBus;
        }

        public IEnumerable<BaseEvent> GetEvents(Guid aggregateId)
        {
            var events = _events.Where(p => p.AggregateId == aggregateId).ToList();
            if (!events.Any())
            {
                throw new AggregateNotFoundException(string.Format("Aggregate with Id: {0} was not found", aggregateId));
            }
            return events;
        }

        public T GetMemento<T>(Guid aggregateId) where T : BaseMemento
        {
            var memento = _mementos.Where(m => m.Id == aggregateId).Select(m => m).LastOrDefault();
            if (memento != null)
                return (T)memento;
            return null;
        }

        public async Task SaveAsync(AggregateRoot aggregate)
        {
            var uncommittedChanges = aggregate.GetUncommittedChanges();
            var version = aggregate.Version;

            foreach (var @event in uncommittedChanges)
            {
                version++;
                if (version > 2)
                {
                    if (version % 3 == 0)
                    {
                        var originator = (IOriginator)aggregate;
                        var memento = originator.GetMemento();
                        SaveMemento(memento);
                    }
                }
                @event.Version = version;
                _events.Add(@event);
            }
            foreach (var @event in uncommittedChanges)
            {
                var desEvent = (dynamic)Convert.ChangeType(@event, @event.GetType());
                await _eventBus.PublishAsync(desEvent);
            }
        }

        public void SaveMemento(BaseMemento memento)
        {
            _mementos.Add(memento);
        }
    }
}
