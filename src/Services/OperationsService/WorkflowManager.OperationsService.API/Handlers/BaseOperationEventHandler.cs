using System.Threading.Tasks;
using MassTransit;
using WorkflowManager.CQRS.Domain.Events;
using WorkflowManager.OperationsStorage.Api.Services;

namespace WorkflowManager.OperationsStorage.API.Handlers
{
    public abstract class BaseOperationEventHandler
    {
        protected readonly IOperationPublisher _operationPublisher;
        protected readonly IOperationsStorage _operationsStorage;

        public BaseOperationEventHandler(IOperationPublisher operationPublisher, IOperationsStorage operationsStorage)
        {
            _operationPublisher = operationPublisher;
            _operationsStorage = operationsStorage;
        }

        protected async Task HandleAsync(ConsumeContext<IEvent> context)
        {
            await _operationsStorage.SetAsync(context.Message);
            await _operationPublisher.PublishResult(context.Message);
        }

    }
}
