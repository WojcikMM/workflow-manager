using System.Threading.Tasks;
using CQRS.Template.Domain.EventHandlers;
using CQRS.Template.ReadModel;
using WorkflowManager.ProductService.Core.Events;
using WorkflowManager.ProductService.Core.ReadModel.Models;

namespace WorkflowManager.ProductService.Core.EventHandlers
{
    public class ProcessCreatedEventHandler : BaseEventHandler<ProcessCreatedEvent, ProcessReadModel>
    {
        public ProcessCreatedEventHandler(IReadModelRepository<ProcessReadModel> readModelRepository) : base(readModelRepository)
        {
        }

        public override async Task HandleAsync(ProcessCreatedEvent handle)
        {
            await _readModelRepository.Add(new ProcessReadModel
            {
                Id = handle.AggregateId,
                Name = handle.Name,
                Version = handle.Version
            });
        }
    }
}
