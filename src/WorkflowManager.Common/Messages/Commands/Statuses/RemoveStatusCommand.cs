using System;
using WorkflowManager.CQRS.Domain.Commands;

namespace WorkflowManager.Common.Messages.Commands.Statuses
{
    public class RemoveStatusCommand : ICommand
    {
        public Guid Id { get; }
        public int Version { get; }

        public RemoveStatusCommand(Guid Id, int Version)
        {
            this.Id = Id;
            this.Version = Version;
        }
    }
}
