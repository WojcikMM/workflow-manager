using CQRS.Template.Domain.Commands;
using System;
using System.Threading.Tasks;

namespace CQRS.Template.Domain.CommandHandlers
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, Guid correlationId);
    }
}
