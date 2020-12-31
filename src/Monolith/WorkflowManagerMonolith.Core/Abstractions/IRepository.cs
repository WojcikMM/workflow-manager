using System;
using System.Threading.Tasks;

namespace WorkflowManagerMonolith.Core.Abstractions
{
    public interface IRepository<T>
    {
        Task<T> GetAsync(Guid id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
