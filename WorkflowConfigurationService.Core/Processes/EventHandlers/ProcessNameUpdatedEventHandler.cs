using System.Threading.Tasks;
using CQRS.Template.ReadModel;
using WorkflowConfigurationService.Core.Processes.Events;
using WorkflowConfigurationService.Core.ReadModel.Models;

namespace WorkflowConfigurationService.Core.Processes.EventHandlers
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
                Name = handle.Name
            });
        }
    }
}
