using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MassTransit;
using WorkflowManager.Common.Messages;
using WorkflowManager.Common.Messages.Events.Processes;
using WorkflowManager.CQRS.ReadModel;
using WorkflowManager.ConfigurationService.ReadModel.ReadDatabase.Models;

namespace WorkflowManager.ConfigurationService.Core.EventHandlers.Processes
{
    public class ProcessRemovedEventHandler : BaseEventHandler<ProcessRemovedEvent>
    {
        private readonly IReadModelRepository<ProcessModel> _repository;

        public ProcessRemovedEventHandler([NotNull]IReadModelRepository<ProcessModel> repository) => _repository = repository;

        public override async Task Consume(ConsumeContext<ProcessRemovedEvent> context) =>
            await _repository.RemoveAsync(context.Message.AggregateId);
    }
}
