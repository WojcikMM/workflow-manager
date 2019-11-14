using System;
using CQRS.Template.Domain.Domain;

namespace CQRS.Template.Domain.Storage
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot: AggregateRoot,new () // ??
    {
        TAggregateRoot GetById(Guid id);
        void Save(TAggregateRoot aggregate, int expectedVersion);

    }
}
