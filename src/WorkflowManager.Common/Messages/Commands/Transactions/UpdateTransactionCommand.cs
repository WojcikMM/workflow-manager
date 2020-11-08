using System;
using Newtonsoft.Json;
using WorkflowManager.CQRS.Commands;

namespace WorkflowManager.Common.Messages.Commands.Transactions
{
    public class UpdateTransactionCommand : BaseCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? StatusId { get; set; }
        public Guid? OutgoingStatusId { get; set; }

        public UpdateTransactionCommand(Guid AggregateId, string Name, string Description, Guid? StatusId, Guid? OutgoingStatusId, int Version) : base(AggregateId, Version)
        {
            this.Name = Name;
            this.Description = Description;
            this.StatusId = StatusId;
            this.OutgoingStatusId = OutgoingStatusId;
        }

        [JsonConstructor]
        public UpdateTransactionCommand(Guid AggregateId, string Name, string Description, Guid? StatusId, Guid? OutgoingStatusId, int Version, Guid CorrelationId) :
            base(AggregateId, Version, CorrelationId)
        {
            this.Name = Name;
            this.Description = Description;
            this.StatusId = StatusId;
            this.OutgoingStatusId = OutgoingStatusId;
        }

    }
}
