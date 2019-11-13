using System;
using WorkflowConfigurationService.Domain.Commands;
using WorkflowConfigurationService.Infrastructure;

namespace WorkflowConfiguration.Infrastructure.Commands
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
