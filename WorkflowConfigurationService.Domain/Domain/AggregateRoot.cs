using System;
using System.Collections.Generic;
using System.Globalization;
using WorkflowConfigurationService.Domain.Events;

namespace WorkflowConfigurationService.Domain.Domain
{
    public class AggregateRoot
    {
        private readonly List<BaseEvent> _changes;
        public Guid Id { get; internal set; }
        public int Version { get; internal set; }
        public int EventVersion { get; protected set; }

        protected AggregateRoot()
        {
            _changes = new List<BaseEvent>();
        }

        public IEnumerable<BaseEvent> GetUncommitedChanges()
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
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event), "Cannot apply null valued event.");
            }

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
            aggregate.Handle(Convert.ChangeType(@event, @event.GetType(),new CultureInfo("en-US")));
        }

    }
}
