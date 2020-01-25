using System;

namespace WorkflowManager.Gateway.API.Commands
{
    public class CreateStatusCommandDTO
    {
        public string Name { get; set; }
        public Guid ProcessId { get; set; }
    }
}
