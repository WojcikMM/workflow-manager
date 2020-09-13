using WorkflowManager.Common.Messages;
using WorkflowManager.Common.Messages.Commands.Processes;
using WorkflowManager.CQRS.Storage;
using WorkflowManager.ConfigurationService.Core.Domain;
using MassTransit;

namespace WorkflowManager.ConfigurationService.Core.CommandHandlers
{
    public class UpdateProcessCommandHandler : BaseCommandHandler<UpdateProcessCommand, Process>
    {
        public UpdateProcessCommandHandler(IRepository<Process> repository) : base(repository)
        {
        }

        public override void HandleCommand(UpdateProcessCommand command, ConsumeContext<UpdateProcessCommand> context)
        {
            aggregate = _repository.GetById(command.AggregateId);
            if (aggregate.Name != command.Name)
            {
                aggregate.UpdateName(command.Name);
            }
        }
    }
}
