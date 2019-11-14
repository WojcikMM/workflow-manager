using System;
using System.Collections.Generic;
using System.Globalization;
using CQRS.Template.Domain.Events;

namespace CQRS.Template.Domain.Domain
{
    public abstract class AggregateRoot
    {
        private readonly List<BaseEvent> _changes;
        public Guid AggregateId { get; protected set; }
        public int Version { get; protected set; }
        public int EventVersion { get; protected set; }

        protected AggregateRoot()
        {
            _changes = new List<BaseEvent>();
        }

        public IEnumerable<BaseEvent> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommited()
        {
            _changes.Clear();
        }

        public void LoadsFromHistory(IEnumerable<BaseEvent> eventsHistory)
        {
            if (eventsHistory is null)
            {
                throw new ArgumentNullException(nameof(eventsHistory), "Cannot load changes from null valued history.");
            }
            foreach (BaseEvent @event in eventsHistory)
            {
                ApplyEventChanges(@event);
            }
        }

        protected void ApplyEvent(BaseEvent @event)
        {
            ApplyEventChanges(@event);
            _changes.Add(@event);
        }


        private void ApplyEventChanges(BaseEvent @event)
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
