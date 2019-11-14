using CQRS.Template.Domain.Events;
using System.Threading.Tasks;

namespace CQRS.Template.Domain.Bus
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T @event) where T : BaseEvent;
    }
}
