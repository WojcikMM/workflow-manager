using System;

namespace CQRS.Template.Domain.Commands
{

    public interface ICommand
    {
        Guid Id { get; }
        int Version { get; }
    }

    [Serializable]
    public class BaseCommand : ICommand
    {
        public Guid Id { get; private set; }
        public int Version { get; private set; }

        public BaseCommand(Guid id, int version)
        {
            Id = id;
            Version = version;
        }
    }
}
