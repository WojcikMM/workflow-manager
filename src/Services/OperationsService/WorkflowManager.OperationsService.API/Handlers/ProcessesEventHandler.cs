using System.Threading.Tasks;
using MassTransit;
using WorkflowManager.Common.Messages.Events.Processes;
using WorkflowManager.Common.Messages.Events.Processes.Complete;
using WorkflowManager.Common.Messages.Events.Processes.Rejected;
using WorkflowManager.OperationsStorage.Api.Services;

namespace WorkflowManager.OperationsStorage.API.Handlers
{

    public class ProcessesEventHandler : BaseOperationEventHandler,

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

        public ProcessesEventHandler(IOperationPublisher operationPublisher, IOperationsStorage operationsStorage) : base(operationPublisher, operationsStorage) { }

        public async Task Consume(ConsumeContext<ProcessCreatedEvent> context) => await HandleAsync(context);

        public async Task Consume(ConsumeContext<ProcessNameUpdatedEvent> context) => await HandleAsync(context);

        public async Task Consume(ConsumeContext<ProcessRemovedEvent> context) => await HandleAsync(context);

        public async Task Consume(ConsumeContext<ProcessNameUpdateRejectedEvent> context) => await HandleAsync(context);

        public async Task Consume(ConsumeContext<ProcessCreateRejectedEvent> context) => await HandleAsync(context);

        public async Task Consume(ConsumeContext<ProcessRemoveRejectedEvent> context) => await HandleAsync(context);

        public async Task Consume(ConsumeContext<ProcessCreateCompleteEvent> context) => await HandleAsync(context);

        public async Task Consume(ConsumeContext<ProcessNameUpdateCompleteEvent> context) => await HandleAsync(context);

        public async Task Consume(ConsumeContext<ProcessRemoveCompleteEvent> context) => await HandleAsync(context);
    }
}
