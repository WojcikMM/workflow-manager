using System;
using CQRS.Template.ReadModel;
using CQRS.Template.Domain.EventHandlers;
using WorkflowConfigurationService.Core.Processes.Events;
using WorkflowConfigurationService.Core.ReadModel.Models;
using System.Threading.Tasks;

namespace WorkflowConfigurationService.Core.Processes.EventHandlers
{
    public class ProcessCreatedEventHandler : IEventHandler<ProcessCreatedEvent>
    {
        private readonly IReadModelRepository<ProcessReadModel> _readModelRepository;

        public ProcessCreatedEventHandler(IReadModelRepository<ProcessReadModel> readModelRepository)
        {
            _readModelRepository = readModelRepository ?? throw new ArgumentNullException(nameof(readModelRepository));
        }

        public async Task HandleAsync(ProcessCreatedEvent handle)
        {
            await _readModelRepository.Add(new ProcessReadModel
            {
                Id = handle.AggregateId,
                Name = handle.Name
            });
        }
    }
}
