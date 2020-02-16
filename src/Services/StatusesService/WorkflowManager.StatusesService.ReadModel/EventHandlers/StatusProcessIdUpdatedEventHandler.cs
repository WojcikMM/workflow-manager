using System;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using WorkflowManager.CQRS.ReadModel;
using WorkflowManager.CQRS.Domain.EventHandlers;
using WorkflowManager.Common.Messages.Events.Statuses;
using WorkflowManager.StatusesService.ReadModel.ReadDatabase;

namespace WorkflowManager.StatusesService.ReadModel.EventHandlers
{
    public class StatusProcessIdUpdatedEventHandler : IEventHandler<StatusProcessIdUpdatedEvent>
    {
        private readonly IReadModelRepository<StatusModel> _repository;

        public StatusProcessIdUpdatedEventHandler([NotNull]IReadModelRepository<StatusModel> repository) => _repository = repository;

        public async Task HandleAsync(StatusProcessIdUpdatedEvent @event, Guid correlationId)
        {
            var status = await _repository.GetByIdAsync(@event.AggregateId);
            status.ProcessId = @event.ProcessId;
            status.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(status);
        }
    }
}
