using System;
using CQRS.Template.Domain.Storage;
using CQRS.Template.Domain.CommandHandlers;
using WorkflowConfigurationService.Core.Processes.Domain;
using WorkflowConfigurationService.Core.Processes.Commands;

namespace WorkflowConfigurationService.Core.Processes.CommandHandlers
{
    public class CreateProcessCommandHandler : ICommandHandler<CreateProcessCommand>
    {
        private readonly IRepository<Process> _repository;

        public CreateProcessCommandHandler(IRepository<Process> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Handle(CreateProcessCommand command)
        {
            if(command is null)
            {
                throw new ArgumentNullException(nameof(command), "Passed command cannot be null.");
            }

            var newProcess = new Process(command.Id, command.Name); //TODO: It is ok to pass version here?

            _repository.Save(newProcess, command.Version);
        }
    }
}
