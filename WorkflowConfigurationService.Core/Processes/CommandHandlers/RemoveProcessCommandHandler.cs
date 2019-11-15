using CQRS.Template.Domain.Storage;
using CQRS.Template.Domain.CommandHandlers;
using WorkflowConfigurationService.Core.Processes.Domain;
using WorkflowConfigurationService.Core.Processes.Commands;


namespace WorkflowConfigurationService.Core.Processes.CommandHandlers
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
