using System;
using WorkflowManagerMonolith.Core.Entities;

namespace WorkflowManagerMonolith.Core.Repository
{
    public interface IRepository<T>
    {
        T Add(T applicationEntity);
        T Get(Guid id);
        T Update(T applicationEntity);
        void Delete(Guid id);
    }
}
