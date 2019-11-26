using CQRS.Template.Domain.Storage;
using CQRS.Template.Domain.CommandHandlers;
using WorkflowManager.ProductService.Core.Domain;
using WorkflowManager.Common.Messages.Commands.Processes;

namespace WorkflowManager.ProductService.Core.CommandHandlers
{
    public class UpdateProcessCommandHandler : BaseCommandHandler<UpdateProcessCommand, Process>
    {
        public UpdateProcessCommandHandler(IRepository<Process> repository) : base(repository)
        {
        }

        public override void HandleCommand(UpdateProcessCommand command)
        {
            aggregate = _repository.GetById(command.Id);
            if (aggregate.Name != command.Name)
            {
                aggregate.UpdateName(command.Name);
            }
        }
    }
}
