using WorkflowManager.CQRS.Domain.EventHandlers;
using WorkflowManager.CQRS.ReadModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using WorkflowManager.Common.Messages.Events.Processes;
using WorkflowManager.ProcessService.ReadModel.ReadDatabase;
using System;

namespace WorkflowManager.ProductService.Core.EventHandlers
{
    public class ProcessNameUpdatedEventHandler : IEventHandler<ProcessNameUpdatedEvent>
    {
        private readonly IReadModelRepository<ProcessModel> _repository;

        public ProcessNameUpdatedEventHandler([NotNull]IReadModelRepository<ProcessModel> repository) =>
            _repository = repository;

        public async Task HandleAsync(ProcessNameUpdatedEvent @event, Guid correlationId)
        {

            ProcessModel process = await _repository.GetByIdAsync(@event.AggregateId);
            process.Name = @event.Name;
            process.Version = @event.Version;

            await _repository.UpdateAsync(process);
        }
    }
}
