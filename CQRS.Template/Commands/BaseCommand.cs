using System;

namespace CQRS.Template.Domain.Commands
{
    [Serializable]
    public class BaseCommand
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
