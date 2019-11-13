using System;
using WorkflowConfigurationService.Domain.Commands;

namespace WorkflowConfiguration.Infrastructure.Commands
{
    public class UpdateProcessCommand : BaseCommand
    {
        public string Name { get; set; }
        public UpdateProcessCommand(Guid Id, string Name, int Version) : base(Id, Version)
        {
            this.Name = Name;
        }
    }
}
