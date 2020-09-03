using System;
using Newtonsoft.Json;
using WorkflowManager.CQRS.Commands;
using WorkflowManager.CQRS.Domain;

namespace WorkflowManager.Common.Messages.Commands.Processes
{
    public class CreateProcessCommand : BaseCommand
    {
        public string Name { get; }

        public CreateProcessCommand(string Name) : base(Guid.NewGuid(), DomainConstants.NewAggregateVersion)
        {
            this.Name = Name;
        }

        [JsonConstructor]
        public CreateProcessCommand(Guid AggregateId, string Name, int Version, Guid CorrelationId) : base(AggregateId, Version, CorrelationId)
        {
            this.Name = Name;
        }
    }
}
