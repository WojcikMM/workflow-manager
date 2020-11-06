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
    public class StatusProcessIdUpdatedEventHandler : BaseEventHandler<StatusProcessIdUpdatedEvent>
    {
        private readonly IReadModelRepository<StatusModel> _repository;

        public StatusProcessIdUpdatedEventHandler([NotNull]IReadModelRepository<StatusModel> repository) => _repository = repository;

        public override async Task Consume(ConsumeContext<StatusProcessIdUpdatedEvent> context)
        {
            var status = await _repository.GetByIdAsync(context.Message.AggregateId);
            status.ProcessId = context.Message.ProcessId;
            status.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(status);
        }
    }
}
