using CQRS.Template.Domain.CommandHandlers;
using CQRS.Template.Domain.Storage;
using WorkflowManager.Common.Messages.Commands.Processes;
using WorkflowManager.ProductService.Core.Domain;

namespace WorkflowManager.ProductService.Core.CommandHandlers
{
    public class RemoveProcessCommandHandler : BaseCommandHandler<RemoveProcessCommand, Process>
    {
        public RemoveProcessCommandHandler(IRepository<Process> repository) : base(repository)
        {
        }

        public override void HandleCommand(RemoveProcessCommand command)
        {
            aggregate = _repository.GetById(command.Id);
            aggregate.Delete();
        }
    }
}
