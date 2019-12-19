using System;
using WorkflowManager.CQRS.ReadModel;

namespace WorkflowManager.StatusService.ReadModel.ReadDatabase
{
    public class StatusModel : IReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProcessId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Version { get; set; }
    }
}
