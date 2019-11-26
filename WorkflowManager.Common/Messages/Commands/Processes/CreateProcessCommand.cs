using System;
using CQRS.Template.Domain.Commands;

namespace WorkflowManager.Common.Messages.Commands.Processes
{
    public class CreateProcessCommand : BaseCommand
    {
        public string Name { get; private set; }

        public CreateProcessCommand(Guid Id, string Name) : base(Id, -1)
        {
            this.Name = Name;
        }      
    }
}
