using System;
using WorkflowManager.Common.Messages.Events.Statuses;
using WorkflowManager.CQRS.Domain.Domain;
using WorkflowManager.CQRS.Domain.Domain.Mementos;
using WorkflowManager.CQRS.Domain.Events;
using WorkflowManager.CQRS.Storage.Mementos;
using WorkflowManager.StatusesService.Core.Domain.Mementos;

namespace WorkflowManager.StatusesService.Core.Domain
{
    public class Status : AggregateRoot, IOriginator,
        IAggregateEventHandler<StatusCreatedEvent>,
        IAggregateEventHandler<StatusNameUpdatedEvent>,
        IAggregateEventHandler<StatusProcessIdUpdatedEvent>,
        IAggregateEventHandler<StatusRemovedEvent>
    {

        public Guid ProcessId { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Only for internal Aggregate use.
        /// </summary>
        public Status() { }

        // Aggregate Methods
        public Status(Guid Id, Guid ProcessId, string Name) => ApplyEvent(new StatusCreatedEvent(Id, Name, ProcessId));

        public void UpdateName(string Name) => ApplyEvent(new StatusNameUpdatedEvent(AggregateId, Name));

        public void UpdateProcessId(Guid ProcessId) => ApplyEvent(new StatusProcessIdUpdatedEvent(AggregateId, ProcessId));

        public void Remove() => ApplyEvent(new StatusRemovedEvent(AggregateId));

        // Event Handlers

        public void HandleEvent(StatusCreatedEvent @event)
        {
            this.AggregateId = @event.AggregateId;
            this.Version = @event.Version;
            this.Name = @event.Name;
            this.ProcessId = @event.ProcessId;
        }

        public void HandleEvent(StatusNameUpdatedEvent @event)
        {
            this.Name = @event.Name;
            this.Version = @event.Version;
        }

        public void HandleEvent(StatusProcessIdUpdatedEvent @event)
        {
            this.ProcessId = @event.ProcessId;
            this.Version = @event.Version;
        }

        public void HandleEvent(StatusRemovedEvent @event)
        {

        }

        // Memento

        public BaseMemento GetMemento()
        {
            return new StatusMemento(AggregateId, Name, ProcessId, Version);
        }

        public void SetMemento(BaseMemento memento)
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento), "Passed memento value is null");
            }

            AggregateId = memento.Id;
            Version = memento.Version;
            Name = ((StatusMemento)memento).Name;
            ProcessId = ((StatusMemento)memento).ProcessId;
        }
    }
}
