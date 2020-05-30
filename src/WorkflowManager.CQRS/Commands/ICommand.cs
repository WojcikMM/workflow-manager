using System;

namespace WorkflowManager.CQRS.Domain.Commands
{
    public interface ICommand
    {
        Guid Id { get; }
        int Version { get; }
    }
}
