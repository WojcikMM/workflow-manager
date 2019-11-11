using System.Threading.Tasks;
using WorkflowConfigurationService.Domain.Commands;

namespace WorkflowConfigurationService.Domain.Bus
{
    public interface ICommandBus
    {
        Task Send<TCommand>(TCommand command) where TCommand : BaseCommand;
    }
}