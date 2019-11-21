using System.Threading.Tasks;
using CQRS.Template.Domain.EventHandlers;
using CQRS.Template.ReadModel;
using WorkflowManager.ProductService.Core.Events;
using WorkflowManager.ProductService.Core.ReadModel.Models;

namespace WorkflowManager.ProductService.Core.EventHandlers
{
    public class ProcessRemovedEventHandler : BaseEventHandler<ProcessRemovedEvent, ProcessReadModel>
    {
        public ProcessRemovedEventHandler(IReadModelRepository<ProcessReadModel> readModelRepository) : base(readModelRepository)
        {
        }

        public override async Task HandleAsync(ProcessRemovedEvent handle)
        {
           await _readModelRepository.Remove(handle.AggregateId);
        }
    }
}
