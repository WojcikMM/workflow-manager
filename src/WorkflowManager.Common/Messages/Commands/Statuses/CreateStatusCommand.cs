using System;
using Newtonsoft.Json;
using WorkflowManager.CQRS.Commands;
using WorkflowManager.CQRS.Domain;

namespace WorkflowManager.Common.Messages.Commands.Statuses
{
    public class CreateStatusCommand : BaseCommand
    {
        public string Name { get; }
        public Guid ProcessId { get; }

        public CreateStatusCommand(string Name, Guid ProcessId) : base(Guid.NewGuid(), DomainConstants.NewAggregateVersion)
        {
            this.Name = Name;
            this.ProcessId = ProcessId;
        }

        [JsonConstructor]
        public CreateStatusCommand(Guid AggregateId, string Name, Guid ProcessId, int Version, Guid CorrelationId) : base(AggregateId, Version, CorrelationId)
        {
            this.Name = Name;
            this.ProcessId = ProcessId;
        }
    }
}
