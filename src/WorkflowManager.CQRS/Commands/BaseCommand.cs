using System;

namespace WorkflowManager.CQRS.Domain.Commands
{

    public interface ICommand
    {
        Guid Id { get; }
        int Version { get; }
    }

    [Serializable]
    public class BaseCommand : ICommand
    {
        public Guid Id { get; }
        public int Version { get; }

        public BaseCommand(Guid id, int version)
        {
            Id = id;
            Version = version;
        }
    }
}
