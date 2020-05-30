using System;
using WorkflowManager.CQRS.Domain.Commands;

namespace WorkflowManager.Common.Messages.Commands.Statuses
{
    public class UpdateStatusCommand : ICommand
    {
        public Guid Id { get; }
        public string Name { get; }
        public Guid? ProcessId { get; }
        public int Version { get; }

        public UpdateStatusCommand(Guid Id, string Name, Guid? ProcessId, int Version)
        {
            this.Id = Id;
            this.Name = Name;
            this.ProcessId = ProcessId;
            this.Version = Version;
        }
    }
}
