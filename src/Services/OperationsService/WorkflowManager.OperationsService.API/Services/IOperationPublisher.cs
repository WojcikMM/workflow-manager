using System;
using System.Threading.Tasks;
using WorkflowManager.CQRS.Domain.Events;

namespace WorkflowManager.OperationsStorage.Api.Services
{
    public interface IOperationPublisher
    {
        Task PublishResult(IEvent @event);
    }
}
