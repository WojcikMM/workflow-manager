using System;

namespace WorkflowManager.Gateway.API.Commands
{
    public class UpdateStatusCommandDTO
    {
        public string Name { get; set; }
        public Guid? ProcessId { get; set; }
        public int Version { get; set; }
    }
}
