using System;
using System.Collections.Generic;
using CQRS.Template.Domain.Domain;
using CQRS.Template.Domain.Domain.Mementos;
using CQRS.Template.Domain.Events;

namespace CQRS.Template.Domain.Storage
{
    public interface IEventStorage
    {
        IEnumerable<BaseEvent> GetEvents(Guid aggregateId);
        void Save(AggregateRoot aggregate);
        T GetMemento<T>(Guid aggregateId) where T : BaseMemento;
        void SaveMemento(BaseMemento memento);
    }
}
