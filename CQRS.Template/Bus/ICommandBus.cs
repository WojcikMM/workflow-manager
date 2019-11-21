using System;
using System.Threading.Tasks;
using CQRS.Template.Domain.Commands;

namespace CQRS.Template.Domain.Bus
{
    public interface ICommandBus
    {
        Task Send<TCommand>(TCommand command, Guid correlationId) where TCommand : BaseCommand;
    }
}