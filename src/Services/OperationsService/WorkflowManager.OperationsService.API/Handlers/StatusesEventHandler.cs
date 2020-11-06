using System.Threading.Tasks;
using MassTransit;
using WorkflowManager.Common.Messages.Events.Statuses;
using WorkflowManager.Common.Messages.Events.Statuses.Rejected;
using WorkflowManager.OperationsStorage.Api.Services;

namespace WorkflowManager.OperationsStorage.API.Handlers
{
    public class StatusesEventHandler : BaseOperationEventHandler,

        IConsumer<StatusCreatedEvent>,
        IConsumer<StatusNameUpdatedEvent>,
        IConsumer<StatusProcessIdUpdatedEvent>,
        IConsumer<StatusRemovedEvent>,

        IConsumer<StatusNotCreatedBecauseWrongProcessId>,
        IConsumer<StatusNotUpdatedBecauseWrongProcessId>
    {
        public StatusesEventHandler(IOperationPublisher operationPublisher, IOperationsStorage operationsStorage) : base(operationPublisher, operationsStorage) { }

        public Task Consume(ConsumeContext<StatusCreatedEvent> context) => HandleAsync(context);

        public Task Consume(ConsumeContext<StatusNameUpdatedEvent> context) => HandleAsync(context);

        public Task Consume(ConsumeContext<StatusProcessIdUpdatedEvent> context) => HandleAsync(context);

        public Task Consume(ConsumeContext<StatusRemovedEvent> context) => HandleAsync(context);

        // Rejection Events

        public Task Consume(ConsumeContext<StatusNotCreatedBecauseWrongProcessId> context) => HandleAsync(context);

        public Task Consume(ConsumeContext<StatusNotUpdatedBecauseWrongProcessId> context) => HandleAsync(context);

    }
}
