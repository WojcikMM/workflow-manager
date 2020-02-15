using WorkflowManager.CQRS.Domain.CommandHandlers;
using WorkflowManager.CQRS.Domain.Storage;
using WorkflowManager.Common.Messages.Commands.Processes;
using WorkflowManager.ProcessesService.Core.Domain;

namespace WorkflowManager.ProcessesService.Core.CommandHandlers
{
    public class RemoveProcessCommandHandler : BaseCommandHandler<RemoveProcessCommand, Process>
    {
        public RemoveProcessCommandHandler(IRepository<Process> repository) : base(repository)
        {
        }

        public override void HandleCommand(RemoveProcessCommand command)
        {
            aggregate = _repository.GetById(command.Id);
            aggregate.Delete();
        }
    }
}
