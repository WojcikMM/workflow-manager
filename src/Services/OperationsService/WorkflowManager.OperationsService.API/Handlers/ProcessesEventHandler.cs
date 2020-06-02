using System.Threading.Tasks;
using MassTransit;
using WorkflowManager.Common.Messages.Events.Processes;
using WorkflowManager.Common.Messages.Events.Processes.Complete;
using WorkflowManager.Common.Messages.Events.Processes.Rejected;
using WorkflowManager.CQRS.Domain.Events;
using WorkflowManager.OperationsStorage.Api.Services;

namespace WorkflowManager.OperationsStorage.API.Handlers
{
    public class ProcessesEventHandler<T> :
        IConsumer<ProcessCreatedEvent>,
        IConsumer<ProcessNameUpdatedEvent>,
        IConsumer<ProcessRemovedEvent>,

        IConsumer<ProcessCreateRejectedEvent>,
        IConsumer<ProcessNameUpdateRejectedEvent>,
        IConsumer<ProcessRemoveRejectedEvent>,

        IConsumer<ProcessCreateCompleteEvent>,
        IConsumer<ProcessNameUpdateCompleteEvent>,
        IConsumer<ProcessRemoveCompleteEvent>
    {
        private readonly IOperationPublisher _operationPublisher;
        private readonly IOperationsStorage _operationsStorage;

        public ProcessesEventHandler(IOperationPublisher operationPublisher, IOperationsStorage operationsStorage)
        {
            _operationPublisher = operationPublisher;
            _operationsStorage = operationsStorage;
        }

        public async Task Consume(ConsumeContext<ProcessCreatedEvent> context) => await HandleAsync(context.Message);

        public async Task Consume(ConsumeContext<ProcessNameUpdatedEvent> context) => await HandleAsync(context.Message);

        public async Task Consume(ConsumeContext<ProcessRemovedEvent> context) => await HandleAsync(context.Message);

        public async Task Consume(ConsumeContext<ProcessNameUpdateRejectedEvent> context) => await HandleAsync(context.Message);

        public async Task Consume(ConsumeContext<ProcessCreateRejectedEvent> context) => await HandleAsync(context.Message);

        public async Task Consume(ConsumeContext<ProcessRemoveRejectedEvent> context) => await HandleAsync(context.Message);

        public async Task Consume(ConsumeContext<ProcessCreateCompleteEvent> context) => await HandleAsync(context.Message);

        public async Task Consume(ConsumeContext<ProcessNameUpdateCompleteEvent> context) => await HandleAsync(context.Message);

        public async Task Consume(ConsumeContext<ProcessRemoveCompleteEvent> context) => await HandleAsync(context.Message);

        private async Task HandleAsync(IEvent @event)
        {
            await _operationsStorage.SetAsync(@event);
            await _operationPublisher.PublishResult(@event);
        }
    }
}
