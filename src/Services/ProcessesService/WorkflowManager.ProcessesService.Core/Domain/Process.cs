using WorkflowManager.CQRS.Domain.Domain;
using WorkflowManager.CQRS.Domain.Domain.Mementos;
using WorkflowManager.CQRS.Domain.Events;
using System;
using WorkflowManager.Common.Messages.Events.Processes;
using WorkflowManager.ProcessesService.Core.Domain.Mementos;

namespace WorkflowManager.ProcessesService.Core.Domain
{
    public class Process : AggregateRoot, IOriginator,
        IAggregateEventHandler<ProcessCreatedEvent>,
        IAggregateEventHandler<ProcessNameUpdatedEvent>,
        IAggregateEventHandler<ProcessRemovedEvent>
    {
        public string Name { get; private set; }

        // Aggregate Methods

        public Process() { }
        public Process(Guid Id, string Name) => ApplyEvent(new ProcessCreatedEvent(Id, Name));

        public void UpdateName(string Name) => ApplyEvent(new ProcessNameUpdatedEvent(AggregateId, Name));

        public void Delete() => ApplyEvent(new ProcessRemovedEvent(AggregateId));

        // Event Handlers

        public void HandleEvent(ProcessCreatedEvent @event)
        {
            AggregateId = @event.AggregateId;
            Name = @event.Name;
            Version = @event.Version;
        }
        public void HandleEvent(ProcessNameUpdatedEvent @event)
        {
            Name = @event.Name;
        }
        public void HandleEvent(ProcessRemovedEvent @event)
        {

        }

        // Memento 
        public BaseMemento GetMemento()
        {
            return new ProcessMemento(AggregateId, Name, Version);
        }

        public void SetMemento(BaseMemento memento)
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento), "Passed memento value is null");
            }

            AggregateId = memento.Id;
            Version = memento.Version;
            Name = ((ProcessMemento)memento).Name;
        }
    }
}
