using System.Threading.Tasks;
using CQRS.Template.Domain.Commands;

namespace CQRS.Template.Domain.CommandHandlers
{
    public interface ICommandHandler<TCommand> where TCommand : BaseCommand
    {
        Task Handle(TCommand command);
    }
}
