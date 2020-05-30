using System;
using WorkflowManager.CQRS.Domain;
using WorkflowManager.CQRS.Domain.Commands;

namespace WorkflowManager.Common.Messages.Commands.Processes
{
    public class CreateProcessCommand : ICommand
    {
        public Guid Id { get; }
        public string Name { get; }
        public int Version { get; }

        public CreateProcessCommand(Guid Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
            Version = DomainConstants.NewAggregateVersion;
        }
    }
}
