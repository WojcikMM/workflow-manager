using WorkflowManager.Common.Messages.Commands.Statuses;
using WorkflowManager.CQRS.Domain.CommandHandlers;
using WorkflowManager.CQRS.Domain.Storage;
using WorkflowManager.StatusesService.Core.Domain;

namespace WorkflowManager.StatusesService.Core.CommandHandlers
{
    public class CreateStatusCommandHandler : BaseCommandHandler<CreateStatusCommand, Status>
    {
        public CreateStatusCommandHandler(IRepository<Status> repository) : base(repository)
        {
        }

        public override void HandleCommand(CreateStatusCommand command)
        {
            aggregate = new Status(command.Id, command.ProcessId, command.Name);
        }
    }
}
