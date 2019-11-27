using System;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using CQRS.Template.Domain.EventHandlers;
using WorkflowManager.Common.Messages.Events.Processes;
using WorkflowManager.ProcessService.ReadModel.ReadDatabase;
using CQRS.Template.ReadModel;

namespace WorkflowManager.ProductService.Core.EventHandlers
{
    public class ProcessCreatedEventHandler : IEventHandler<ProcessCreatedEvent>
    {
        private readonly IReadModelRepository<ProcessModel> _repository;

        public ProcessCreatedEventHandler([NotNull]IReadModelRepository<ProcessModel> repository) => _repository = repository;

        public async Task HandleAsync(ProcessCreatedEvent @event)
        {
            var process = new ProcessModel()
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
