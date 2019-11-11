using System;
using System.Collections.Generic;
using WorkflowConfigurationService.Domain.Domain;
using WorkflowConfigurationService.Domain.Domain.Mementos;
using WorkflowConfigurationService.Domain.Events;

namespace WorkflowConfigurationService.Domain.Storage
{
    public interface IEventStorage
    {
        IEnumerable<BaseEvent> GetEvents(Guid aggregateId);
        void Save(AggregateRoot aggregate);
        T GetMemento<T>(Guid aggregateId) where T : BaseMemento;
        void SaveMemento(BaseMemento memento);
    }
}
