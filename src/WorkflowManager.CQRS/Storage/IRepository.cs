using WorkflowManager.CQRS.Domain.Domain;
using System;
using System.Threading.Tasks;

namespace WorkflowManager.CQRS.Storage
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot, new() // ??
    {
        bool Any(Guid id);
        TAggregateRoot GetById(Guid id);
        Task SaveAsync(TAggregateRoot aggregate, int expectedVersion, Guid correlationId);

    }
}
