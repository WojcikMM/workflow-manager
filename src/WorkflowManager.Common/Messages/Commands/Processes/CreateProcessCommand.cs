using WorkflowManager.CQRS.Domain.Commands;
using System;

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
