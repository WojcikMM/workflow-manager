using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MassTransit;
using WorkflowManager.Common.Messages;
using WorkflowManager.Common.Messages.Events.Statuses;
using WorkflowManager.CQRS.ReadModel;
using WorkflowManager.ConfigurationService.ReadModel.ReadDatabase.Models;

namespace WorkflowManager.ConfigurationService.ReadModel.EventHandlers.Statuses
{
    public class StatusNameUpdatedEventHandler : BaseEventHandler<StatusNameUpdatedEvent>
    {
        private readonly IReadModelRepository<StatusModel> _repository;

        public StatusNameUpdatedEventHandler([NotNull]IReadModelRepository<StatusModel> repository) => _repository = repository;

        public override async Task Consume(ConsumeContext<StatusNameUpdatedEvent> context)
        {
            var status = await _repository.GetByIdAsync(context.Message.AggregateId);
            status.Name = context.Message.Name;
            status.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(status);
        }
    }
}
