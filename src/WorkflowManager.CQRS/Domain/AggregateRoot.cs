using System;
using System.Linq;
using System.Collections.Generic;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.CQRS.Domain.Domain
{
    public abstract class AggregateRoot
    {
        private readonly List<IEvent> _changes;
        public Guid AggregateId { get; protected set; }
        public int Version { get; protected set; }
        public int EventVersion { get; protected set; }

        protected AggregateRoot()
        {
            _changes = new List<IEvent>();
        }

        public IEnumerable<IEvent> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommited()
        {
            _changes.Clear();
        }

        public void LoadsFromHistory(IEnumerable<IEvent> eventsHistory)
        {
            if (eventsHistory is null)
            {
                throw new ArgumentNullException(nameof(eventsHistory), "Cannot load changes from null valued history.");
            }
            foreach (IEvent @event in eventsHistory)
            {
                ApplyEventChanges(@event);
            }
            Version = eventsHistory.Last().Version;

        }

        protected void ApplyEvent(IEvent @event)
        {
            ApplyEventChanges(@event);
            _changes.Add(@event);
        }


        private void ApplyEventChanges(IEvent @event)
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event), "Cannot apply null valued event.");
            }

            dynamic aggregate = this;
            aggregate.HandleEvent((dynamic)Convert.ChangeType(@event, @event.GetType()));
        }

    }
}
