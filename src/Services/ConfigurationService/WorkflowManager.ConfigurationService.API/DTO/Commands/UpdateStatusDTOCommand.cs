using System;

namespace WorkflowManager.ConfigurationService.API.DTO.Commands
{
    public class UpdateStatusDTOCommand
    {
        public string Name { get; set; }
        public Guid? ProcessId { get; set; }
        public int Version { get; set; }

    }
}
