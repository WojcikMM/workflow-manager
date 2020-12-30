using WorkflowManager.CQRS.Domain.Domain;
using System;
using System.Threading.Tasks;

namespace WorkflowManager.CQRS.Storage
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot, new() // ??
    {
        Task<bool> AnyAsync(Guid id);
        Task<TAggregateRoot> GetByIdAsync(Guid id);
        Task SaveAsync(TAggregateRoot aggregate, int expectedVersion, Guid correlationId);

    }
}
