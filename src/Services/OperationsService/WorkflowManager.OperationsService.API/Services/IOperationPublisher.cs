using System;
using System.Threading.Tasks;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.OperationsStorage.Api.Services
{
    public interface IOperationPublisher
    {
        Task PendingAsync(Guid correlationId, IEvent @event);
        Task CompleteAsync(Guid correlationId, IEvent @event);
        Task RejectAsync(Guid correlationContext, IRejectedEvent @event);
    }
}
