using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MassTransit;
using WorkflowManager.Common.Messages;
using WorkflowManager.Common.Messages.Events.Processes;
using WorkflowManager.CQRS.ReadModel;
using WorkflowManager.ProcessesService.ReadModel.ReadDatabase;

namespace WorkflowManager.ProcessesService.Core.EventHandlers
{
    public class ProcessNameUpdatedEventHandler : BaseEventHandler<ProcessNameUpdatedEvent>
    {
        private readonly IReadModelRepository<ProcessModel> _repository;

        public ProcessNameUpdatedEventHandler([NotNull]IReadModelRepository<ProcessModel> repository) =>
            _repository = repository;

        public override async Task Consume(ConsumeContext<ProcessNameUpdatedEvent> context)
        {
            var @event = context.Message;
            ProcessModel process = await _repository.GetByIdAsync(@event.AggregateId);
            process.Name = @event.Name;
            process.Version = @event.Version;

            await _repository.UpdateAsync(process);
        }
    }
}
