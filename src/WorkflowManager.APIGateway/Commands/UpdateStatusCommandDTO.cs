using System;

namespace WorkflowManagerGateway.Commands
{
    public class UpdateStatusCommandDTO
    {
        public string Name { get; set; }
        public Guid? ProcessId { get; set; }
        public int Version { get; set; }
    }
}
