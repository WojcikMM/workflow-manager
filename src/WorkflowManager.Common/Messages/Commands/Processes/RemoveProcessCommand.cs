using System;
using WorkflowManager.CQRS.Domain.Commands;

namespace WorkflowManager.Common.Messages.Commands.Processes
{
    public class RemoveProcessCommand : ICommand
    {
        public Guid Id { get; }
        public int Version { get; }

        public RemoveProcessCommand(Guid Id, int Version)
        {
            this.Id = Id;
            this.Version = Version;
        }
    }
}
