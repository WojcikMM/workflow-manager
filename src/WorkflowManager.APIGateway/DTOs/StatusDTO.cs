using System;

namespace WorkflowManagerGateway.DTOs
{
    public class StatusDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProcessId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Version { get; set; }
    }
}
