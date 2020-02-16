using System;
using WorkflowManager.CQRS.Domain.Domain.Mementos;

namespace WorkflowManager.StatusesService.Core.Domain.Mementos
{
    public class StatusMemento : BaseMemento
    {
        public string Name { get; }
        public Guid ProcessId { get; }


        public StatusMemento(Guid Id, string Name, Guid ProcessId, int Version) : base(Id, Version)
        {
            this.Name = Name;
            this.ProcessId = ProcessId;
        }
    }
}
