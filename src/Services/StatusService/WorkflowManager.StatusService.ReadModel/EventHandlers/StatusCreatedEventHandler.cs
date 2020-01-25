using System;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using WorkflowManager.CQRS.ReadModel;
using WorkflowManager.CQRS.Domain.EventHandlers;
using WorkflowManager.Common.Messages.Events.Statuses;
using WorkflowManager.StatusService.ReadModel.ReadDatabase;

namespace WorkflowManager.StatusService.ReadModel.EventHandlers
{
    public class StatusCreatedEventHandler : IEventHandler<StatusCreatedEvent>
    {
        private readonly IReadModelRepository<StatusModel> _repository;

        public StatusCreatedEventHandler([NotNull]IReadModelRepository<StatusModel> repository) => _repository = repository;

        public async Task HandleAsync(StatusCreatedEvent @event, Guid correlationId)
        {
            StatusModel process = new StatusModel()
            {
                Id = @event.AggregateId,
                Name = @event.Name,
                ProcessId = @event.ProcessId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Version = @event.Version
            };

            await _repository.AddAsync(process);
        }
    }
}
