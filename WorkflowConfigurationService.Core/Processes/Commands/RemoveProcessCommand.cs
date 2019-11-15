using System;
using CQRS.Template.Domain.Commands;

namespace WorkflowConfigurationService.Core.Processes.Commands
{
    public class RemoveProcessCommand : BaseCommand
    {
        public RemoveProcessCommand(Guid Id, int Version):base(Id,Version)
        {

        }
    }
}
