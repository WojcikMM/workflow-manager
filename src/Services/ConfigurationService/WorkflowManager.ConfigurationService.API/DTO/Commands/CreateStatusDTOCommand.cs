using System;

namespace WorkflowManager.ConfigurationService.API.DTO.Commands
{
    public class CreateStatusDTOCommand
    {
        public string Name { get; set; }
        public Guid ProcessId { get; set; }
    }
}
