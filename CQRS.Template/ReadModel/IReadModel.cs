using System;

namespace CQRS.Template.ReadModel
{
    public interface IReadModel
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
    }
}
