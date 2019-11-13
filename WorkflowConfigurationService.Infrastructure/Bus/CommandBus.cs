using System;
using WorkflowConfigurationService.Domain.Commands;
using WorkflowConfigurationService.Domain.CommandHandlers;
using Microsoft.Extensions.DependencyInjection;
using WorkflowConfigurationService.Domain.Exceptions;
using System.Threading.Tasks;
using WorkflowConfigurationService.Domain.Bus;

namespace WorkflowConfigurationService.Infrastructure.Bus
{
    public class CommandBus : ICommandBus
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandBus(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }


        public async Task Send<T>(T command) where T : BaseCommand
        {
            var commandHandler = _serviceProvider.GetService<ICommandHandler<T>>();
            if (commandHandler is null)
            {
                throw new UnregisteredDomainCommandException("Cannot find handler for this method");
            }
            else
            {
               await Task.Run(() =>  commandHandler.Handle(command));
            }
        }
    }
}
