using WorkflowManager.CQRS.Domain.Domain.Mementos;
using System;

namespace WorkflowManager.ConfigurationService.Core.Domain.Mementos
{
    public class ProcessMemento : BaseMemento
    {
        public ProcessMemento(Guid Id, string Name, int Version) : base(Id, Version)
        {
            this.Name = Name ?? throw new ArgumentNullException(nameof(Name));
        }

        public string Name { get; set; }

    }
}
