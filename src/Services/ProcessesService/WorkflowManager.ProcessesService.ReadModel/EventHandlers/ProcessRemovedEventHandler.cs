using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MassTransit;
using WorkflowManager.Common.Messages;
using WorkflowManager.Common.Messages.Events.Processes;
using WorkflowManager.CQRS.ReadModel;
using WorkflowManager.ProcessesService.ReadModel.ReadDatabase;

namespace WorkflowManager.ProcessesService.Core.EventHandlers
{
    public class ProcessRemovedEventHandler : BaseEventHandler<ProcessRemovedEvent>
    {
        private readonly IReadModelRepository<ProcessModel> _repository;

        public ProcessRemovedEventHandler([NotNull]IReadModelRepository<ProcessModel> repository) => _repository = repository;

        public override async Task Consume(ConsumeContext<ProcessRemovedEvent> context) =>
            await _repository.RemoveAsync(context.Message.AggregateId);
    }
}
