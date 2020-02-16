using WorkflowManager.CQRS.Domain.CommandHandlers;
using WorkflowManager.CQRS.Domain.Storage;
using WorkflowManager.Common.Messages.Commands.Processes;
using WorkflowManager.ProcessesService.Core.Domain;

namespace WorkflowManager.ProcessesService.Core.CommandHandlers
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
