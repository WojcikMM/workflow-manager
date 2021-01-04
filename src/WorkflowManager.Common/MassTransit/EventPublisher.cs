using MassTransit;
using System.Threading.Tasks;
using WorkflowManager.CQRS.Domain.Events;
using WorkflowManager.CQRS.Events;

namespace WorkflowManager.Common.MassTransit
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventPublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task PublishEvent(IEvent @event)
        {
            _publishEndpoint.Publish(@event, @event.GetType());
            return Task.CompletedTask;
        }
    }
}
