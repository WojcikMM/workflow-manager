using System.Threading.Tasks;
using CQRS.Template.Domain.EventHandlers;
using CQRS.Template.ReadModel;
using WorkflowManager.ProductService.Core.Events;
using WorkflowManager.ProductService.Core.ReadModel.Models;

namespace WorkflowManager.ProductService.Core.EventHandlers
{
    public class ProcessNameUpdatedEventHandler : BaseEventHandler<ProcessNameUpdatedEvent, ProcessReadModel>
    {
        public ProcessNameUpdatedEventHandler(IReadModelRepository<ProcessReadModel> readModelRepository) : base(readModelRepository)
        {
        }

        public async override Task HandleAsync(ProcessNameUpdatedEvent handle)
        {
            await _readModelRepository.Update(new ProcessReadModel
            {
                Id = handle.AggregateId,
                Name = handle.Name,
                Version = handle.Version
            });
        }
    }
}
