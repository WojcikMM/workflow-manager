using WorkflowManager.Common.Messages;
using WorkflowManager.Common.Messages.Commands.Processes;
using WorkflowManager.CQRS.Storage;
using WorkflowManager.ConfigurationService.Core.Domain;
using MassTransit;

namespace WorkflowManager.ConfigurationService.Core.CommandHandlers
{
    public class CreateProcessCommandHandler : BaseCommandHandler<CreateProcessCommand, Process>
    {
        public CreateProcessCommandHandler(IRepository<Process> repository) : base(repository)
        {
        }

        public override void HandleCommand(CreateProcessCommand command, ConsumeContext<CreateProcessCommand> context)
        {
            aggregate = new Process(command.AggregateId, command.Name);
        }
    }
}
