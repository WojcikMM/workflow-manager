using System.Threading.Tasks;

namespace WorkflowManagerMonolith.Core.Abstractions
{
    public interface IUseCaseHandler<in TRequest, out TResponse> where TResponse : Task
    {
        TResponse HandleAsync(TRequest data);
    }
}
