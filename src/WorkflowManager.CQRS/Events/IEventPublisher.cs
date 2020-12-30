using System.Threading.Tasks;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.CQRS.Events
{
    public interface IEventPublisher
    {
        Task PublishEvent(IEvent @event);
    }
}
