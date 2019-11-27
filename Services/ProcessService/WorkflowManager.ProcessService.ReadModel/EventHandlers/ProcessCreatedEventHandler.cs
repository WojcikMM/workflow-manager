using CQRS.Template.Domain.EventHandlers;
using CQRS.Template.ReadModel;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using WorkflowManager.Common.Messages.Events.Processes;
using WorkflowManager.ProcessService.ReadModel.ReadDatabase;

namespace WorkflowManager.ProductService.Core.EventHandlers
{
    public class ProcessCreatedEventHandler : IEventHandler<ProcessCreatedEvent>
    {
        private readonly IReadModelRepository<ProcessModel> _repository;

        public ProcessCreatedEventHandler([NotNull]IReadModelRepository<ProcessModel> repository) => _repository = repository;

        public async Task HandleAsync(ProcessCreatedEvent @event)
        {
            ProcessModel process = new ProcessModel()
            {
                Id = @event.AggregateId,
                Name = @event.Name,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Version = @event.Version
            };

            await _repository.AddAsync(process);
        }
    }
}
