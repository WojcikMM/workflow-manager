using CQRS.Template.Domain.Commands;
using System;

namespace WorkflowManagerGateway.Commands
{

    public class UpdateProcessCommandDTO
    {
        public string Name { get; set; }
        public int Version { get; set; }
    }


    public class UpdateProcessCommand : UpdateProcessCommandDTO, ICommand
    {
        public UpdateProcessCommand(Guid id, UpdateProcessCommandDTO createProductCommandDTO)
        {
            Name = createProductCommandDTO.Name;
            Version = createProductCommandDTO.Version;
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
