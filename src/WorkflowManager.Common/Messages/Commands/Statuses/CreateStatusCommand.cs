using System;
using WorkflowManager.CQRS.Domain;
using WorkflowManager.CQRS.Domain.Commands;

namespace WorkflowManager.Common.Messages.Commands.Statuses
{
    public class CreateStatusCommand : ICommand
    {
        public Guid Id { get; }
        public string Name { get; }
        public Guid ProcessId { get; }
        public int Version { get; }

        public CreateStatusCommand(Guid Id, string Name, Guid ProcessId)
        {
            this.Id = Id;
            this.Name = Name;
            this.ProcessId = ProcessId;
            this.Version = DomainConstants.NewAggregateVersion;
        }
    }
}
