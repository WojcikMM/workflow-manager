using System.Threading.Tasks;
using CQRS.Template.ReadModel;
using WorkflowConfigurationService.Core.Processes.Events;
using WorkflowConfigurationService.Core.ReadModel.Models;

namespace WorkflowConfigurationService.Core.Processes.EventHandlers
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
