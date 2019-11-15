using System;
using CQRS.Template.Domain.Domain;
using CQRS.Template.Domain.Events;
using CQRS.Template.Domain.Domain.Mementos;
using WorkflowConfigurationService.Core.Processes.Events;
using WorkflowConfigurationService.Core.Processes.Domain.Mementos;

namespace WorkflowConfigurationService.Core.Processes.Domain
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
            this.AggregateId = @event.AggregateId;
            this.Name = @event.Name;
            this.Version = @event.Version;
        }
        public void HandleEvent(ProcessNameUpdatedEvent @event)
        {
            this.Name = @event.Name;
        }
        public void HandleEvent(ProcessRemovedEvent @event)
        {

        }

        // Memento 
        public BaseMemento GetMemento()
        {
            return new ProcessMemento(this.AggregateId, this.Name, this.Version);
        }

        public void SetMemento(BaseMemento memento)
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento), "Passed memento value is null");
            }

            this.AggregateId = memento.Id;
            this.Version = memento.Version;
            this.Name = ((ProcessMemento)memento).Name;
        }
    }
}
