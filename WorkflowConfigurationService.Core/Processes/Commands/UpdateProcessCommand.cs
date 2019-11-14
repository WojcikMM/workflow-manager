using System;
using CQRS.Template.Domain.Commands;

namespace WorkflowConfigurationService.Core.Processes.Commands
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
