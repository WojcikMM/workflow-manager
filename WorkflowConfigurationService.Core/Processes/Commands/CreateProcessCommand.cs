using System;
using CQRS.Template.Domain.Commands;
using WorkflowConfigurationService.Infrastructure;

namespace WorkflowConfigurationService.Core.Processes.Commands
{
    public class CreateProcessCommand : BaseCommand
    {
        public string Name { get; private set; }

        public CreateProcessCommand(Guid Id, string Name) : base(Id, DomainConstants.NewAggregateVersion)
        {
            this.Name = Name;
        }      
    }
}
