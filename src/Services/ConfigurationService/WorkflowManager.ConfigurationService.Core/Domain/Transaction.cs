using System;
using WorkflowManager.Common.Messages.Events.Transactions;
using WorkflowManager.ConfigurationService.Core.Domain.Mementos;
using WorkflowManager.CQRS.Domain.Domain;
using WorkflowManager.CQRS.Domain.Domain.Mementos;
using WorkflowManager.CQRS.Domain.Events;
using WorkflowManager.CQRS.Storage.Mementos;

namespace WorkflowManager.ConfigurationService.Core.Domain
{
    public class Transaction : AggregateRoot, IOriginator,
        IAggregateEventHandler<TransactionCreatedEvent>,
        IAggregateEventHandler<TransactionNameUpdatedEvent>,
        IAggregateEventHandler<TransactionDescriptionUpdatedEvent>,
        IAggregateEventHandler<TransactionStatusIdUpdatedEvent>,
        IAggregateEventHandler<TransactionOutgoingStatusIdUpdatedEvent>
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public Guid StatusId { get; set; }
        public Guid OutgoingStatusId { get; set; }

        /// <summary>
        /// Only for internal Aggregate use.
        /// </summary>
        public Transaction() { }

        // Aggregate Methods
        public Transaction(Guid Id, string Name, string Description, Guid StatusId, Guid OutgoingStatusId) =>
            ApplyEvent(new TransactionCreatedEvent(AggregateId, Name, Description, StatusId, OutgoingStatusId));

        public void UpdateName(string Name) =>
            ApplyEvent(new TransactionNameUpdatedEvent(this.AggregateId, Name));

        public void UpdateDescription(string Description) =>
            ApplyEvent(new TransactionDescriptionUpdatedEvent(this.AggregateId, Description));
        public void UpdateStatusId(Guid StatusId) =>
            ApplyEvent(new TransactionStatusIdUpdatedEvent(this.AggregateId, StatusId));
        public void UpdateResultStatusId(Guid OutgoingStatusId) =>
            ApplyEvent(new TransactionOutgoingStatusIdUpdatedEvent(this.AggregateId, OutgoingStatusId));

        //Event handlers

        public void HandleEvent(TransactionCreatedEvent @event)
        {
            this.AggregateId = @event.AggregateId;
            this.Name = @event.Name;
            this.Description = @event.Description;
            this.StatusId = @event.StatusId;
            this.OutgoingStatusId = @event.OutgoingStatusId;
            this.Version = @event.Version;
        }

        public void HandleEvent(TransactionNameUpdatedEvent @event)
        {
            this.Name = @event.Name;
            this.Version = @event.Version;
        }

        public void HandleEvent(TransactionDescriptionUpdatedEvent @event)
        {
            this.Description = @event.Description;
            this.Version = @event.Version;
        }

        public void HandleEvent(TransactionStatusIdUpdatedEvent @event)
        {
            this.StatusId = @event.StatusId;
            this.Version = @event.Version;
        }

        public void HandleEvent(TransactionOutgoingStatusIdUpdatedEvent @event)
        {
            this.OutgoingStatusId = @event.OutgoingStatusId;
            this.Version = @event.Version;
        }

        // Memento
        public BaseMemento GetMemento()
        {
            return new TransactionMemento(AggregateId, Name, Description, StatusId, OutgoingStatusId, Version);
        }

        public void SetMemento(BaseMemento memento)
        {
            if (memento is null)
            {
                throw new ArgumentNullException(nameof(memento), "Passed memento value is null");
            }

            var transactionMemento = memento as TransactionMemento;

            AggregateId = transactionMemento.Id;
            Name = transactionMemento.Name;
            Description = transactionMemento.Description;
            StatusId = transactionMemento.StatusId;
            OutgoingStatusId = transactionMemento.OutgoingStatusId;
            Version = transactionMemento.Version;
        }
    }
}
