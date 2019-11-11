using System;
using WorkflowConfigurationService.Domain.Commands;

namespace WorkflowConfigurationService.Infrastructure.Commands.Application
{
    public class CreateApplicationCommand : BaseCommand
    {
        public CreateApplicationCommand(Guid id, int version) : base(id, version)
        {
        }
    }
}
