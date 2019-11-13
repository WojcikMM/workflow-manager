using System;
using WorkflowConfigurationService.Domain.Domain.Mementos;
using WorkflowConfigurationService.Domain.Events;
using WorkflowConfigurationService.Domain.Events.Processes;

namespace WorkflowConfigurationService.Domain.Domain
{
    public class Process : AggregateRoot, IOriginator,
        IAggregateEventHandler<ProcessCreatedEvent>
    {
        public string Name { get; private set; }


        public Process() { }
        public Process(Guid Id, string Name)
        {
            ApplyEvent(new ProcessCreatedEvent(Id, Name));
        }



        public void HandleEvent(ProcessCreatedEvent @event)
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event), "Passed event value is null");
            }

            this.Id = @event.AggregateId;
            this.Name = @event.Name;
            this.Version = @event.Version;
        }

        public BaseMemento GetMemento()
        {
            return new ProcessMemento(this.Id, this.Name, this.Version);
        }

        public void SetMemento(BaseMemento memento)
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento), "Passed memento value is null");
            }

            this.Id = memento.Id;
            this.Version = memento.Version;
            this.Name = ((ProcessMemento)memento).Name;
        }
    }
}
