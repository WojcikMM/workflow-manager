using System;
using WorkflowManager.ConfigurationService.Core.Domain.Mementos;
using WorkflowManager.CQRS.Domain.Domain;
using WorkflowManager.CQRS.Domain.Domain.Mementos;
using WorkflowManager.CQRS.Storage.Mementos;

namespace WorkflowManager.ConfigurationService.Core.Domain
{
    //TODO: Try private event handlers (methods)
    public class Transaction : AggregateRoot, IOriginator
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public Guid StatusId { get; set; }
        public Guid ResultStatusId { get; set; }

        /// <summary>
        /// Only for internal Aggregate use.
        /// </summary>
        public Transaction() { }

        // Aggregate Methods
        public Transaction(Guid Id, string Name, string Description, Guid StatusId) => ApplyEvent(null);

        public void UpdateName(string Name) => ApplyEvent(null);

        public void UpdateDescription(string Description) => ApplyEvent(null);
        public void UpdateStatusId(Guid StatusId) => ApplyEvent(null);
        public void UpdateResultStatusId(Guid ResultStatusId) => ApplyEvent(null);

        //Event handlers

        //TODO: Add Events and Event handlers for transaction aggregate


        // Memento
        public BaseMemento GetMemento()
        {
            return new TransactionMemento(AggregateId, Name, Description, StatusId, ResultStatusId, Version);
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
            ResultStatusId = transactionMemento.ResultStatusId;
            Version = transactionMemento.Version;
        }
    }
}
