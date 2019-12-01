using System;

namespace WorkflowManager.CQRS.Domain.Domain.Mementos
{
    public abstract class BaseMemento
    {
        protected BaseMemento(Guid Id, int Version)
        {
            this.Id = Id;
            this.Version = Version;
        }

        public Guid Id { get; internal set; }
        public int Version { get; internal set; }
    }
}
