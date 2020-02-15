using WorkflowManager.CQRS.ReadModel;
using System;

namespace WorkflowManager.ProcessesService.ReadModel.ReadDatabase
{
    public class ProcessModel : IReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Version { get; set; }
    }
}
