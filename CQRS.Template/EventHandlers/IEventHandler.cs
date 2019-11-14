using CQRS.Template.Domain.Events;
using System.Threading.Tasks;

namespace CQRS.Template.Domain.EventHandlers
{
    public interface IEventHandler<TEvent> where TEvent : BaseEvent
    {
        Task HandleAsync(TEvent handle);
    }
}
