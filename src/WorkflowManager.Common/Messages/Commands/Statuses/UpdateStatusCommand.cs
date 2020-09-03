using System;
using Newtonsoft.Json;
using WorkflowManager.CQRS.Commands;

namespace WorkflowManager.Common.Messages.Commands.Statuses
{
    public class UpdateStatusCommand : BaseCommand
    {
        public string Name { get; }
        public Guid? ProcessId { get; }

        public UpdateStatusCommand(Guid AggregateId, string Name, Guid? ProcessId, int Version) : base(AggregateId, Version)
        {
            this.Name = Name;
            this.ProcessId = ProcessId;
        }

        [JsonConstructor]
        public UpdateStatusCommand(Guid AggregateId, string Name, Guid? ProcessId, int Version, Guid CorrelationId) : base(AggregateId, Version, CorrelationId)
        {
            this.Name = Name;
            this.ProcessId = ProcessId;
        }
    }
}
