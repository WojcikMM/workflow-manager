using System;
using Newtonsoft.Json;
using WorkflowManager.CQRS.Commands;
using WorkflowManager.CQRS.Domain;

namespace WorkflowManager.Common.Messages.Commands.Transactions
{
    public class CreateTransactionCommand: BaseCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid StatusId { get; set; }
        public Guid OutgoingStatusId { get; set; }

        public CreateTransactionCommand(string Name, string Description, Guid StatusId, Guid OutgoingStatusId):base(Guid.NewGuid(), DomainConstants.NewAggregateVersion)
        {
            this.Name = Name;
            this.Description = Description;
            this.StatusId = StatusId;
            this.OutgoingStatusId = OutgoingStatusId;
        }

        [JsonConstructor]
        public CreateTransactionCommand(Guid AggregateId,string Name, string Description, Guid StatusId, Guid OutgoingStatusId, int Version, Guid CorrelationId) :
            base(AggregateId, Version, CorrelationId)
        {
            this.Name = Name;
            this.Description = Description;
            this.StatusId = StatusId;
            this.OutgoingStatusId = OutgoingStatusId;
        }        
    }
}
