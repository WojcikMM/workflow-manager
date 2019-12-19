using System;
using WorkflowManager.CQRS.Domain.Commands;

namespace WorkflowManager.Common.Messages.Commands.Statuses
{
    public class RemoveStatusCommand : BaseCommand
    {
        public RemoveStatusCommand(Guid Id, int Version) : base(Id, Version)
        {
        }
    }
}
