using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using CQRS.Template.ReadModel;
using CQRS.Template.Domain.EventHandlers;
using WorkflowManager.Common.Messages.Events.Processes;
using WorkflowManager.ProcessService.ReadModel.ReadDatabase;

namespace WorkflowManager.ProductService.Core.EventHandlers
{
    public class ProcessNameUpdatedEventHandler : IEventHandler<ProcessNameUpdatedEvent>
    {
        private readonly IReadModelRepository<ProcessModel> _repository;

        public ProcessNameUpdatedEventHandler([NotNull]IReadModelRepository<ProcessModel> repository) => 
            _repository = repository;

        public async Task HandleAsync(ProcessNameUpdatedEvent @event)
        {

            var process = await _repository.GetByIdAsync(@event.AggregateId);
            process.Name = @event.Name;

            await _repository.Update(process);
        }
    }
}
