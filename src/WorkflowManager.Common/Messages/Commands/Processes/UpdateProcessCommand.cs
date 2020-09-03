using System;
using Newtonsoft.Json;
using WorkflowManager.CQRS.Commands;

namespace WorkflowManager.Common.Messages.Commands.Processes
{
    public class UpdateProcessCommand : BaseCommand
    {
        public string Name { get; }

        public UpdateProcessCommand(Guid AggregateId, string Name, int Version) : base(AggregateId, Version)
        {
            this.Name = Name;
        }

        [JsonConstructor]
        public UpdateProcessCommand(Guid AggregateId, string Name, int Version, Guid CorrelationId) : base(AggregateId, Version, CorrelationId)
        {
            this.Name = Name;
        }
    }
}
