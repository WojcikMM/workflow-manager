using System.Threading.Tasks;
using MassTransit;
using WorkflowManager.Common.Messages.Events.Operations;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.OperationsStorage.Api.Services
{
    public class OperationPublisher : IOperationPublisher
    {
        private readonly IPublishEndpoint _busPublisher;

        public OperationPublisher(IPublishEndpoint busPublisher) => _busPublisher = busPublisher;

        public async Task PublishResult(IEvent @event)
        {
            if (@event is IRejectedEvent)
            {
                var rejectedEvent = @event as IRejectedEvent;
                await _busPublisher.Publish(new OperationRejected()
                {
                    AggregateId = @event.AggregateId,
                    Id = @event.Id,
                    Version = @event.Version,
                    ExceptionMessage = rejectedEvent.ExceptionMessage,
                    BusinessResponse = rejectedEvent.BusinessResponse,
                    ExceptionStack = rejectedEvent.ExceptionStack,
                    CorrelationId = rejectedEvent.CorrelationId
                });
            }
            else if (@event is ICompleteEvent)
            {
                await _busPublisher.Publish(new OperationCompleted()
                {
                    AggregateId = @event.AggregateId,
                    Id = @event.Id,
                    Version = @event.Version,
                    CorrelationId = @event.CorrelationId
                });
            }
            else
            {
                await _busPublisher.Publish(new OperationPending()
                {
                    AggregateId = @event.AggregateId,
                    Id = @event.Id,
                    Version = @event.Version,
                    CorrelationId = @event.CorrelationId
                });
            }
        }
    }
}
