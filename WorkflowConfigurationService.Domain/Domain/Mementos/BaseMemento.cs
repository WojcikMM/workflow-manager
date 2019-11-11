using System;

namespace WorkflowConfigurationService.Domain.Domain.Mementos
{
    public class BaseMemento
    {
        public Guid Id { get; internal set; }
        public int Version { get; internal set; }
    }
}
