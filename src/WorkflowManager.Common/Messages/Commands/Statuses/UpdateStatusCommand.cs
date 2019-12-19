using System;
using WorkflowManager.CQRS.Domain.Commands;

namespace WorkflowManager.Common.Messages.Commands.Statuses
{
    public class UpdateStatusCommand : BaseCommand
    {
        public string Name { get; }
        public Guid? ProcessId { get; }

        public UpdateStatusCommand(Guid Id, string Name, Guid? ProcessId, int Version) : base(Id, Version)
        {
            this.Name = Name;
            this.ProcessId = ProcessId;
        }
    }
}
