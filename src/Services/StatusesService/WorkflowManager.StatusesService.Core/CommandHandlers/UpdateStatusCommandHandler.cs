using WorkflowManager.Common.Messages;
using WorkflowManager.Common.Messages.Commands.Statuses;
using WorkflowManager.CQRS.Storage;
using WorkflowManager.StatusesService.Core.Domain;

namespace WorkflowManager.StatusesService.Core.CommandHandlers
{
    public class UpdateStatusCommandHandler : BaseCommandHandler<UpdateStatusCommand, Status>
    {
        public UpdateStatusCommandHandler(IRepository<Status> repository) : base(repository)
        {
        }

        public override void HandleCommand(UpdateStatusCommand command)
        {
            aggregate = _repository.GetById(command.AggregateId);

            if(!string.IsNullOrWhiteSpace(command.Name)
                && aggregate.Name != command.Name.Trim())
            {
                aggregate.UpdateName(command.Name);
            }

            if(command.ProcessId.HasValue
                && aggregate.ProcessId != command.ProcessId.Value)
            {
                aggregate.UpdateProcessId(command.ProcessId.Value);
            }

        }
    }
}
