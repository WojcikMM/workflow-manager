using System;
using CQRS.Template.Domain.Domain.Mementos;

namespace WorkflowManager.ProductService.Core.Domain.Mementos
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
