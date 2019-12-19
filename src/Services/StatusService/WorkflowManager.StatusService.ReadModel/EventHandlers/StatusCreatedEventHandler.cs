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

        public async Task HandleAsync(StatusCreatedEvent @event)
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

    public class StatusNameUpdatedEventHandler : IEventHandler<StatusNameUpdatedEvent>
    {
        private readonly IReadModelRepository<StatusModel> _repository;

        public StatusNameUpdatedEventHandler([NotNull]IReadModelRepository<StatusModel> repository) => _repository = repository;

        public async Task HandleAsync(StatusNameUpdatedEvent @event)
        {
            var status = await _repository.GetByIdAsync(@event.AggregateId);
            status.Name = @event.Name;
            status.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(status);
        }
    }


    public class StatusProcessIdUpdatedEventHandler : IEventHandler<StatusProcessIdUpdatedEvent>
    {
        private readonly IReadModelRepository<StatusModel> _repository;

        public StatusProcessIdUpdatedEventHandler([NotNull]IReadModelRepository<StatusModel> repository) => _repository = repository;

        public async Task HandleAsync(StatusProcessIdUpdatedEvent @event)
        {
            var status = await _repository.GetByIdAsync(@event.AggregateId);
            status.ProcessId = @event.ProcessId;
            status.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(status);
        }
    }


    public class StatusRemovedEventHandler : IEventHandler<StatusRemovedEvent>
    {
        private readonly IReadModelRepository<StatusModel> _repository;

        public StatusRemovedEventHandler([NotNull]IReadModelRepository<StatusModel> repository) => _repository = repository;

        public async Task HandleAsync(StatusRemovedEvent @event)
        {
            await _repository.RemoveAsync(@event.AggregateId);
        }
    }
}
