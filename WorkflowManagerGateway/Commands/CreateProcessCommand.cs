using CQRS.Template.Domain.Commands;
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
            this.Name = createProductCommandDTO.Name;
            this.Id = Guid.NewGuid();
            this.Version = -1;
        }
        public Guid Id { get; set; }
        public int Version { get; set; }
    }
}
