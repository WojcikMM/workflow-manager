using System;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using WorkflowManager.CQRS.ReadModel;
using WorkflowManager.CQRS.Domain.EventHandlers;
using WorkflowManager.Common.Messages.Events.Statuses;
using WorkflowManager.StatusService.ReadModel.ReadDatabase;

namespace WorkflowManager.StatusService.ReadModel.EventHandlers
{
    public class StatusRemovedEventHandler : IEventHandler<StatusRemovedEvent>
    {
        private readonly IReadModelRepository<StatusModel> _repository;

        public StatusRemovedEventHandler([NotNull]IReadModelRepository<StatusModel> repository) => _repository = repository;

        public async Task HandleAsync(StatusRemovedEvent @event, Guid correlationId)
        {
            await _repository.RemoveAsync(@event.AggregateId);
        }
    }
}
