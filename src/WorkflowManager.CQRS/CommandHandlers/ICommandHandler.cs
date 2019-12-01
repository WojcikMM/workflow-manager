using WorkflowManager.CQRS.Domain.Commands;
using System;
using System.Threading.Tasks;

namespace WorkflowManager.CQRS.Domain.CommandHandlers
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, Guid correlationId);
    }
}
