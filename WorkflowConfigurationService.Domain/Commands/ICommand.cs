using System;

namespace WorkflowConfigurationService.Domain.Commands
{
    public interface ICommand
    {
        Guid Id { get; }
        int Version { get; }
    }
}