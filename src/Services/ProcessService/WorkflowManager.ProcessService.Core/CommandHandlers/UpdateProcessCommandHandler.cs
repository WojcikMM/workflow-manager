using WorkflowManager.CQRS.Domain.CommandHandlers;
using WorkflowManager.CQRS.Domain.Storage;
using WorkflowManager.Common.Messages.Commands.Processes;
using WorkflowManager.ProcessesService.Core.Domain;

namespace WorkflowManager.ProcessesService.Core.CommandHandlers
{
    public class UpdateProcessCommandHandler : BaseCommandHandler<UpdateProcessCommand, Process>
    {
        public UpdateProcessCommandHandler(IRepository<Process> repository) : base(repository)
        {
        }

        public override void HandleCommand(UpdateProcessCommand command)
        {
            aggregate = _repository.GetById(command.Id);
            if (aggregate.Name != command.Name)
            {
                aggregate.UpdateName(command.Name);
            }
        }
    }
}
