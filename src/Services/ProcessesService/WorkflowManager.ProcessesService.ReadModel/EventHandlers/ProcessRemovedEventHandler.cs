using WorkflowManager.CQRS.Domain.EventHandlers;
using WorkflowManager.CQRS.ReadModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using WorkflowManager.Common.Messages.Events.Processes;
using WorkflowManager.ProcessesService.ReadModel.ReadDatabase;
using System;

namespace WorkflowManager.ProcessesService.Core.EventHandlers
{
    public class ProcessRemovedEventHandler : IEventHandler<ProcessRemovedEvent>
    {
        private readonly IReadModelRepository<ProcessModel> _repository;

        public ProcessRemovedEventHandler([NotNull]IReadModelRepository<ProcessModel> repository) => _repository = repository;

        public async Task HandleAsync(ProcessRemovedEvent @event, Guid correlationId) =>
            await _repository.RemoveAsync(@event.AggregateId);
    }
}
