using System;
using WorkflowManager.CQRS.Domain.Commands;

namespace WorkflowManager.Common.Messages.Commands.Statuses
{
    public class CreateStatusCommand : BaseCommand
    {
        public string Name { get; }
        public Guid ProcessId { get; }

        public CreateStatusCommand(Guid Id, string Name, Guid ProcessId) : base(Id, -1)
        {
            this.Name = Name;
            this.ProcessId = ProcessId;
        }
    }
}
