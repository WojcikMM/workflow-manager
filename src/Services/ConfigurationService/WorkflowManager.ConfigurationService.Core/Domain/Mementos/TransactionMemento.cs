using System;
using WorkflowManager.CQRS.Domain.Domain.Mementos;

namespace WorkflowManager.ConfigurationService.Core.Domain.Mementos
{
    public class TransactionMemento : BaseMemento
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid StatusId { get; set; }
        public Guid ResultStatusId { get; set; }

        public TransactionMemento(Guid Id, string Name, string Description, Guid StatusId, Guid ResultStatusId, int Version) : base(Id, Version)
        {
            this.Name = Name;
            this.Description = Description;
            this.StatusId = StatusId;
            this.ResultStatusId = ResultStatusId;
        }
    }
}
