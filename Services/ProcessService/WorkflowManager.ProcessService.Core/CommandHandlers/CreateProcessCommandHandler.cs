using CQRS.Template.Domain.Storage;
using CQRS.Template.Domain.CommandHandlers;
using WorkflowManager.ProductService.Core.Domain;
using WorkflowManager.Common.Messages.Commands.Processes;

namespace WorkflowManager.ProductService.Core.CommandHandlers
{
    public class CreateProcessCommandHandler : BaseCommandHandler<CreateProcessCommand, Process>
    {
        public CreateProcessCommandHandler(IRepository<Process> repository) : base(repository)
        {
        }

        public override void HandleCommand(CreateProcessCommand command)
        {
            aggregate = new Process(command.Id, command.Name);
        }
    }
}
