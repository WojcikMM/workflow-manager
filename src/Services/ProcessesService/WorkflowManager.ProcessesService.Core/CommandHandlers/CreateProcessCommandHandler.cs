using WorkflowManager.CQRS.Domain.CommandHandlers;
using WorkflowManager.CQRS.Domain.Storage;
using WorkflowManager.Common.Messages.Commands.Processes;
using WorkflowManager.ProcessesService.Core.Domain;
using MassTransit;
using System.Threading.Tasks;

namespace WorkflowManager.ProcessesService.Core.CommandHandlers
{
    public class CreateProcessCommandHandler : BaseCommandHandler<CreateProcessCommand, Process>, IConsumer<CreateProcessCommand>
    {
        public CreateProcessCommandHandler(IRepository<Process> repository) : base(repository)
        {
        }

        public Task Consume(ConsumeContext<CreateProcessCommand> context)
        {
            throw new System.NotImplementedException();
        }

        public override void HandleCommand(CreateProcessCommand command)
        {
            aggregate = new Process(command.Id, command.Name);
        }
    }
}
