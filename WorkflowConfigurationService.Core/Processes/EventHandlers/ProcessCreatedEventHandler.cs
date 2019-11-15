﻿using System.Threading.Tasks;
using CQRS.Template.ReadModel;
using WorkflowConfigurationService.Core.Processes.Events;
using WorkflowConfigurationService.Core.ReadModel.Models;

namespace WorkflowConfigurationService.Core.Processes.EventHandlers
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
                Name = handle.Name
            });
        }
    }
}
