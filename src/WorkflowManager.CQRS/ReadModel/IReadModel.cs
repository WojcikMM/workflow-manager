using System;

namespace WorkflowManager.CQRS.ReadModel
{
    public interface IReadModel
    {
        public Guid Id { get; }
        public int Version { get; set; }
    }
}
