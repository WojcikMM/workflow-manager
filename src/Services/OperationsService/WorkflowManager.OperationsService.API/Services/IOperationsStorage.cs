using System;
using System.Threading.Tasks;
using WorkflowManager.CQRS.Domain.Events;
using WorkflowManager.OperationsStorage.Api.Dto;

namespace WorkflowManager.OperationsStorage.Api.Services
{
    public interface IOperationsStorage
    {
        Task<OperationDto> GetAsync(Guid id);
        Task SetAsync(IEvent @event);
    }
}
