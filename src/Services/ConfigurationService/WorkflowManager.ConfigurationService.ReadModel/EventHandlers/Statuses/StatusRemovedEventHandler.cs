using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MassTransit;
using WorkflowManager.Common.Messages;
using WorkflowManager.Common.Messages.Events.Statuses;
using WorkflowManager.CQRS.ReadModel;
using WorkflowManager.ConfigurationService.ReadModel.ReadDatabase.Models;

namespace WorkflowManager.ConfigurationService.ReadModel.EventHandlers.Statuses
{
    public class StatusRemovedEventHandler : BaseEventHandler<StatusRemovedEvent>
    {
        private readonly IReadModelRepository<StatusModel> _repository;

        public StatusRemovedEventHandler([NotNull]IReadModelRepository<StatusModel> repository) => _repository = repository;

        public override async Task Consume(ConsumeContext<StatusRemovedEvent> context)
        {
            await _repository.RemoveAsync(context.Message.AggregateId);
        }
    }
}
