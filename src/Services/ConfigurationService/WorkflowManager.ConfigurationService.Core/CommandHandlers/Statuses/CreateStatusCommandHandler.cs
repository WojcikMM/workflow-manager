using WorkflowManager.Common.Messages;
using WorkflowManager.Common.Messages.Commands.Statuses;
using WorkflowManager.CQRS.Storage;
using WorkflowManager.ConfigurationService.Core.Domain;
using WorkflowManager.CQRS.Domain.Exceptions;
using MassTransit;
using WorkflowManager.Common.Messages.Events.Statuses.Rejected;

namespace WorkflowManager.ConfigurationService.Core.CommandHandlers
{
    public class CreateStatusCommandHandler : BaseCommandHandler<CreateStatusCommand, Status>
    {
        public IRepository<Process> _processesRepository { get; }

        public CreateStatusCommandHandler(IRepository<Status> repository, IRepository<Process> processesRepository) : base(repository)
        {
            _processesRepository = processesRepository;
        }

        public override void HandleCommand(CreateStatusCommand command, ConsumeContext<CreateStatusCommand> context)
        {
            if (!_processesRepository.Any(command.ProcessId))
            {
                // Publish rejection event to inform about fail in command handler
                context.Publish(new StatusNotCreatedBecauseWrongProcessId(command.AggregateId));
                throw new AggregateInternalLogicException($"Cannot create this status because given process with Id = {command.ProcessId} not exists.");
            }
            aggregate = new Status(command.AggregateId, command.ProcessId, command.Name);
        }
    }
}
