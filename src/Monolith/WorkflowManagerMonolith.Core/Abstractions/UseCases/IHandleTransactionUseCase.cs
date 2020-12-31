using System.Threading.Tasks;
using WorkflowManagerMonolith.Core.Commands;

namespace WorkflowManagerMonolith.Core.Abstractions.UseCases
{
    public interface IHandleTransactionUseCase : IUseCaseHandler<HandleTransactionCommand, Task> { }
}
