using WorkflowManager.CQRS.Domain.Commands;
using System;

namespace WorkflowManagerGateway.Commands
{
    public class CreateProcessCommandDTO
    {
        public string Name { get; set; }
    }


    public class CreateProcessCommand : CreateProcessCommandDTO, ICommand
    {
        public CreateProcessCommand(CreateProcessCommandDTO createProductCommandDTO)
        {
            Name = createProductCommandDTO.Name;
            Id = Guid.NewGuid();
            Version = -1;
        }
        public Guid Id { get; set; }
        public int Version { get; set; }
    }
}
