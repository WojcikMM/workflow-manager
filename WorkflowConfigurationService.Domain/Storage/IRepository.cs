using System;
using WorkflowConfigurationService.Domain.Domain;

namespace WorkflowConfigurationService.Domain.Storage
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot: AggregateRoot,new () // ??
    {
        TAggregateRoot GetById(Guid id);
        void Save(TAggregateRoot aggregate, int expectedVersion);

    }
}
