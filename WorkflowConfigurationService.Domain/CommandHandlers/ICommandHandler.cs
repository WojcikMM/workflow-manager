using WorkflowConfigurationService.Domain.Commands;

namespace WorkflowConfigurationService.Domain.CommandHandlers
{
    public interface ICommandHandler<TCommand> where TCommand : BaseCommand
    {
        void Handle(TCommand command);
    }
}
