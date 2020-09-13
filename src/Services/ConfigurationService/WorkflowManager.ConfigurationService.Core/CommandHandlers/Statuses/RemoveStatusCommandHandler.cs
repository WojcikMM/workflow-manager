using WorkflowManager.Common.Messages;
using WorkflowManager.Common.Messages.Commands.Statuses;
using WorkflowManager.CQRS.Storage;
using WorkflowManager.ConfigurationService.Core.Domain;
using MassTransit;

namespace WorkflowManager.ConfigurationService.Core.CommandHandlers
{
    public class RemoveStatusCommandHandler : BaseCommandHandler<RemoveStatusCommand, Status>
    {
        public RemoveStatusCommandHandler(IRepository<Status> repository) : base(repository)
        {
        }
        public override void HandleCommand(RemoveStatusCommand command, ConsumeContext<RemoveStatusCommand> context)
        {
            aggregate = _repository.GetById(command.AggregateId);
            aggregate.Remove();
        }
    }
}
