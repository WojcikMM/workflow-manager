using WorkflowManager.Common.Messages;
using WorkflowManager.Common.Messages.Commands.Processes;
using WorkflowManager.CQRS.Storage;
using WorkflowManager.ConfigurationService.Core.Domain;
using MassTransit;

namespace WorkflowManager.ConfigurationService.Core.CommandHandlers
{
    public class RemoveProcessCommandHandler : BaseCommandHandler<RemoveProcessCommand, Process>
    {
        public RemoveProcessCommandHandler(IRepository<Process> repository) : base(repository)
        {
        }

        public override void HandleCommand(RemoveProcessCommand command, ConsumeContext<RemoveProcessCommand> context)
        {
            aggregate = _repository.GetByIdAsync(command.AggregateId).GetAwaiter().GetResult();
            aggregate.Delete();
        }
    }
}
