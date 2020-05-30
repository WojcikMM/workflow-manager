using System;
using WorkflowManager.CQRS.Domain.Commands;

namespace WorkflowManager.Common.Messages.Commands.Processes
{
    public class UpdateProcessCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; }
        public int Version { get; set; }

        public UpdateProcessCommand(Guid Id, string Name, int Version)
        {
            this.Id = Id;
            this.Name = Name;
            this.Version = Version;
        }
    }
}
