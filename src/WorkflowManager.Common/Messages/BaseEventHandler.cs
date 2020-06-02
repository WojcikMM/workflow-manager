using System.Threading.Tasks;
using MassTransit;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.Common.Messages
{
    public abstract class BaseEventHandler<TEvent> : IConsumer<TEvent>
        where TEvent : class, IEvent
    {
        public abstract Task Consume(ConsumeContext<TEvent> context);
    }
}
