using System;

namespace WorkflowManager.ConfigurationService.API.DTO.Commands
{
    public class CreateStatusCommandDto
    {
        public string Name { get; set; }
        public Guid ProcessId { get; set; }
    }
}
