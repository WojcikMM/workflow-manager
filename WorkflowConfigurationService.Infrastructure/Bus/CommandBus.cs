using System;
using System.Threading.Tasks;
using CQRS.Template.Domain.Bus;
using CQRS.Template.Domain.Commands;
using CQRS.Template.Domain.Exceptions;
using CQRS.Template.Domain.CommandHandlers;
using Microsoft.Extensions.DependencyInjection;

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
