using System;
using CQRS.Template.Domain.Storage;
using CQRS.Template.Domain.CommandHandlers;
using WorkflowConfigurationService.Core.Processes.Domain;
using WorkflowConfigurationService.Core.Processes.Commands;


namespace WorkflowConfigurationService.Core.Processes.CommandHandlers
{
    public class UpdateProcessCommandHandler : ICommandHandler<UpdateProcessCommand>
    {
        private readonly IRepository<Process> _repository;

        public UpdateProcessCommandHandler(IRepository<Process> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Handle(UpdateProcessCommand command)
        {
            if(command is null)
            {
                throw new ArgumentNullException(nameof(command), "Passed command value is null.");
            }

            var process = _repository.GetById(command.Id);
            if(process.Name != command.Name)
            {
                process.UpdateName(command.Name);
            }

            _repository.Save(process, command.Version);
        }
    }
}
