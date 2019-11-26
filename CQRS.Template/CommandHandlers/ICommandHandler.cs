using System;
using System.Threading.Tasks;
using CQRS.Template.Domain.Commands;

namespace CQRS.Template.Domain.CommandHandlers
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, Guid correlationId);
    }
}
