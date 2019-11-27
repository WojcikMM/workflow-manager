using CQRS.Template.Domain.Commands;
using System;

namespace WorkflowManager.Common.Messages.Commands.Processes
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
