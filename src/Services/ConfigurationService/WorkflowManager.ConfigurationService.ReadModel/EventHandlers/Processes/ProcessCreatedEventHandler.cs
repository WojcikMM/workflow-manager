using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MassTransit;
using WorkflowManager.Common.Messages;
using WorkflowManager.Common.Messages.Events.Processes;
using WorkflowManager.CQRS.ReadModel;
using WorkflowManager.ConfigurationService.ReadModel.ReadDatabase.Models;

namespace WorkflowManager.ConfigurationService.Core.EventHandlers.Processes
{
    public class ProcessCreatedEventHandler : BaseEventHandler<ProcessCreatedEvent>
    {
        private readonly IReadModelRepository<ProcessModel> _repository;

        public ProcessCreatedEventHandler([NotNull]IReadModelRepository<ProcessModel> repository) => _repository = repository;

        public override async Task Consume(ConsumeContext<ProcessCreatedEvent> context)
        {
            var @event = context.Message;
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
