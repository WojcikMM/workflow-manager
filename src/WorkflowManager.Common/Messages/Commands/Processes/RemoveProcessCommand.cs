using System;
using Newtonsoft.Json;
using WorkflowManager.CQRS.Commands;

namespace WorkflowManager.Common.Messages.Commands.Processes
{
    public class RemoveProcessCommand : BaseCommand
    {
        public RemoveProcessCommand(Guid AggregateId, int Version) : base(AggregateId, Version)
        {
        }

        [JsonConstructor]
        public RemoveProcessCommand(Guid AggregateId, int Version, Guid CorrelationId) : base(AggregateId, Version, CorrelationId)
        {
        }
    }
}
