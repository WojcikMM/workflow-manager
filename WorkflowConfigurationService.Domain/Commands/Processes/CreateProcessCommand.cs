using System;
using WorkflowConfigurationService.Domain.Commands;
using WorkflowConfigurationService.Infrastructure;

namespace WorkflowConfiguration.Infrastructure.Commands
{
    public class CreateProcessCommand : BaseCommand
    {
        public string Name { get; private set; }

        public CreateProcessCommand(Guid id, string name) : base(id, DomainConstants.NewAggregateVersion)
        {
            Name = name;
        }      
    }
}
