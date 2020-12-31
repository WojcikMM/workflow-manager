using System.Threading.Tasks;
using WorkflowManagerMonolith.Core.Commands;
using WorkflowManagerMonolith.Core.Domain;

namespace WorkflowManagerMonolith.Core.Abstractions.UseCases
{
    public interface IGetApplicationUseCase : IUseCaseHandler<GetApplicationCommand, Task<ApplicationEntity>> { }
}
