namespace CQRS.Template.Domain.Domain.Mementos
{
    public interface IOriginator
    {
        BaseMemento GetMemento();
        void SetMemento(BaseMemento memento);
    }
}