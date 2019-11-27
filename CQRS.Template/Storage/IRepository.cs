using CQRS.Template.Domain.Domain;
using System;
using System.Threading.Tasks;

namespace CQRS.Template.Domain.Storage
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot, new() // ??
    {
        TAggregateRoot GetById(Guid id);
        Task SaveAsync(TAggregateRoot aggregate, int expectedVersion, Guid correlationId);

    }
}
