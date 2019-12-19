using WorkflowManager.Common.Messages.Commands.Statuses;
using WorkflowManager.CQRS.Domain.CommandHandlers;
using WorkflowManager.CQRS.Domain.Storage;
using WorkflowManager.StatusService.Core.Domain;

namespace WorkflowManager.StatusService.Core.CommandHandlers
{
    public class UpdateStatusCommandHandler : BaseCommandHandler<UpdateStatusCommand, Status>
    {
        public UpdateStatusCommandHandler(IRepository<Status> repository) : base(repository)
        {
        }

        public override void HandleCommand(UpdateStatusCommand command)
        {
            aggregate = _repository.GetById(command.Id);

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
