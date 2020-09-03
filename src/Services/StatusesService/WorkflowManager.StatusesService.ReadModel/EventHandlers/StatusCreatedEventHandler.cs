using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MassTransit;
using WorkflowManager.Common.Messages;
using WorkflowManager.Common.Messages.Events.Statuses;
using WorkflowManager.CQRS.ReadModel;
using WorkflowManager.StatusesService.ReadModel.ReadDatabase;

namespace WorkflowManager.StatusesService.ReadModel.EventHandlers
{
    public class StatusCreatedEventHandler : BaseEventHandler<StatusCreatedEvent>
    {
        private readonly IReadModelRepository<StatusModel> _repository;

        public StatusCreatedEventHandler([NotNull]IReadModelRepository<StatusModel> repository) => _repository = repository;

        public override async Task Consume(ConsumeContext<StatusCreatedEvent> context)
        {
            var @event = context.Message;
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
