using WorkflowManager.CQRS.Domain.Domain.Mementos;

namespace WorkflowManager.CQRS.Storage.Mementos
{
    public interface IOriginator
    {
        BaseMemento GetMemento();
        void SetMemento(BaseMemento memento);
    }
}