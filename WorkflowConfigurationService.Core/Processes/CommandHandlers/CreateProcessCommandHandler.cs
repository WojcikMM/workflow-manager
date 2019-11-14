using CQRS.Template.Domain.Storage;
using CQRS.Template.Domain.CommandHandlers;
using WorkflowConfigurationService.Core.Processes.Domain;
using WorkflowConfigurationService.Core.Processes.Commands;

namespace WorkflowConfigurationService.Core.Processes.CommandHandlers
{
    public class CreateProcessCommandHandler : BaseCommandHandler<CreateProcessCommand, Process>
    {
        public CreateProcessCommandHandler(IRepository<Process> repository) : base(repository)
        {
        }

        public override void HandleCommand(CreateProcessCommand command)
        {
            aggregate = new Process(command.Id, command.Name);
        }
    }
}
