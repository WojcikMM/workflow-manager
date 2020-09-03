using System;
using Newtonsoft.Json;
using WorkflowManager.CQRS.Commands;

namespace WorkflowManager.Common.Messages.Commands.Statuses
{
    public class RemoveStatusCommand : BaseCommand
    {
        public RemoveStatusCommand(Guid AggregateId, int Version) : base(AggregateId, Version)
        {
        }

        [JsonConstructor]
        public RemoveStatusCommand(Guid AggregateId, int Version, Guid CorrelationId) : base(AggregateId, Version, CorrelationId)
        {
        }
    }
}
