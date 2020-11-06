using System;

namespace WorkflowManager.ConfigurationService.API.DTO.Query
{
    public class ProcessDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Version { get; set; }
    }
}
