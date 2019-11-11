using System;
using WorkflowConfigurationService.Domain.Commands;
using WorkflowConfigurationService.Domain.CommandHandlers;
using Microsoft.Extensions.DependencyInjection;
using WorkflowConfigurationService.Domain.Exceptions;

namespace WorkflowConfigurationService.Domain.Bus
{
    public class CommandBus : ICommandBus
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandBus(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }


        public void Send<T>(T command) where T : BaseCommand
        {
            var commandHandler = _serviceProvider.GetService<ICommandHandler<T>>();
            if (commandHandler is null)
            {
                throw new UnregisteredDomainCommandException("Cannot find handler for this method");
            }
            else
            {
                commandHandler.Handle(command);
            }
        }
    }
}
