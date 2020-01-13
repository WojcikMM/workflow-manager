using System;

namespace WorkflowManagerGateway.Commands
{
    public class CreateStatusCommandDTO
    {
        public string Name { get; set; }
        public Guid ProcessId { get; set; }
    }
}
