using System;

namespace WorkflowManagerMonolith.Web.Shared.Statuses
{
    public class StatusDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
