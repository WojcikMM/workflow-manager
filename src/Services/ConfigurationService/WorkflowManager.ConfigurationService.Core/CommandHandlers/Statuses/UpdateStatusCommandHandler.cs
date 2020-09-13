using WorkflowManager.Common.Messages;
using WorkflowManager.Common.Messages.Commands.Statuses;
using WorkflowManager.CQRS.Storage;
using WorkflowManager.ConfigurationService.Core.Domain;
using WorkflowManager.CQRS.Domain.Exceptions;
using MassTransit;
using WorkflowManager.Common.Messages.Events.Statuses.Rejected;

namespace WorkflowManager.ConfigurationService.Core.CommandHandlers
{
    public class UpdateStatusCommandHandler : BaseCommandHandler<UpdateStatusCommand, Status>
    {
        private readonly IRepository<Process> _processesRepository;

        public UpdateStatusCommandHandler(IRepository<Status> repository, IRepository<Process> processesRepository) : base(repository)
        {
            _processesRepository = processesRepository;
        }

        public override void HandleCommand(UpdateStatusCommand command, ConsumeContext<UpdateStatusCommand> context)
        {
            aggregate = _repository.GetById(command.AggregateId);

            if (!string.IsNullOrWhiteSpace(command.Name)
                && aggregate.Name != command.Name.Trim())
            {
                aggregate.UpdateName(command.Name);
            }

            if (command.ProcessId.HasValue
                && aggregate.ProcessId != command.ProcessId.Value)
            {
                if (!_processesRepository.Any(command.ProcessId.Value))
                {
                    context.Publish(new StatusNotUpdatedBecauseWrongProcessId(command.AggregateId));
                    throw new AggregateInternalLogicException($"Cannot set process Id to {command.ProcessId} because it not exists.");
                }
                aggregate.UpdateProcessId(command.ProcessId.Value);
            }
        }
    }
}
