using System;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using WorkflowManager.CQRS.ReadModel;
using WorkflowManager.CQRS.Domain.EventHandlers;
using WorkflowManager.Common.Messages.Events.Statuses;
using WorkflowManager.StatusesService.ReadModel.ReadDatabase;

namespace WorkflowManager.StatusesService.ReadModel.EventHandlers
{
    public class StatusNameUpdatedEventHandler : IEventHandler<StatusNameUpdatedEvent>
    {
        private readonly IReadModelRepository<StatusModel> _repository;

        public StatusNameUpdatedEventHandler([NotNull]IReadModelRepository<StatusModel> repository) => _repository = repository;

        public async Task HandleAsync(StatusNameUpdatedEvent @event, Guid correlationId)
        {
            var status = await _repository.GetByIdAsync(@event.AggregateId);
            status.Name = @event.Name;
            status.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(status);
        }
    }
}
