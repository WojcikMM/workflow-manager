using CQRS.Template.Domain.Commands;
using System;

namespace WorkflowManager.Common.Messages.Commands.Processes
{
    public class RemoveProcessCommand : BaseCommand
    {
        public RemoveProcessCommand(Guid Id, int Version) : base(Id, Version)
        {

        }
    }
}
