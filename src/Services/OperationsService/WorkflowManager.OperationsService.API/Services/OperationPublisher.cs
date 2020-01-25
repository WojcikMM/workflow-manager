using System;
using System.Threading.Tasks;
using WorkflowManager.Common.Messages.Events.Operations;
using WorkflowManager.Common.RabbitMq;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.OperationsStorage.Api.Services
{
    public class OperationPublisher : IOperationPublisher
    {
        private readonly IBusPublisher _busPublisher;

        public OperationPublisher(IBusPublisher busPublisher) => _busPublisher = busPublisher;

        public async Task PendingAsync(Guid correlationId, IEvent @event) =>
            await _busPublisher.PublishAsync(new OperationPending()
            {
                AggregateId = @event.AggregateId,
                Id = @event.Id,
                Version = @event.Version
            }, correlationId);


        public async Task CompleteAsync(Guid correlationId, IEvent @event) =>
            await _busPublisher.PublishAsync(new OperationCompleted()
            {
                AggregateId = @event.Id,
                Id = @event.Id,
                Version = @event.Version
            }, correlationId);

        public async Task RejectAsync(Guid correlationId, IRejectedEvent @event) =>
            await _busPublisher.PublishAsync(new OperationRejected()
            {
                AggregateId = @event.Id,
                Id = @event.Id,
                Version = @event.Version,
                ExceptionMessage = @event.ExceptionMessage,
                BusinessResponse = @event.BusinessResponse,
                ExceptionStack = @event.ExceptionStack

            }, correlationId);
    }
}
