using System;
using System.Threading.Tasks;
using WorkflowManager.CQRS.Domain.EventHandlers;
using WorkflowManager.CQRS.Domain.Events;
using WorkflowManager.OperationsStorage.Api.Dto;
using WorkflowManager.OperationsStorage.Api.Services;

namespace WorkflowManager.OperationsStorage.API.Handlers
{
    public class GenericEventHandler<T> : IEventHandler<T> where T : class, IEvent
    {
        private readonly IOperationPublisher _operationPublisher;
        private readonly IOperationsStorage _operationsStorage;

        public GenericEventHandler(IOperationPublisher operationPublisher, IOperationsStorage operationsStorage)
        {
            _operationPublisher = operationPublisher;
            _operationsStorage = operationsStorage;
        }


        public async Task HandleAsync(T @event, Guid correlationId)
        {
            var operation = new OperationDto() { AggregateId = @event.AggregateId };

            if(@event is IRejectedEvent)
            {
                operation.Status = "Rejected";
                await _operationsStorage.SetAsync(correlationId, operation);
                await _operationPublisher.RejectAsync(correlationId, @event as IRejectedEvent);
            }
            else
            {
                operation.Status = "Complete";
                await _operationsStorage.SetAsync(correlationId, operation);
                await _operationPublisher.CompleteAsync(correlationId, @event);
            }
        }
    }
}
