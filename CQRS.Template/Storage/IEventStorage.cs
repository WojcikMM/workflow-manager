using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CQRS.Template.Domain.Domain;
using CQRS.Template.Domain.Domain.Mementos;
using CQRS.Template.Domain.Events;

namespace CQRS.Template.Domain.Storage
{
    public interface IEventStorage
    {
        IEnumerable<BaseEvent> GetEvents(Guid aggregateId);
        Task SaveAsync(AggregateRoot aggregate, Guid correlationId);
        T GetMemento<T>(Guid aggregateId) where T : BaseMemento;
        void SaveMemento(BaseMemento memento);
    }
}
